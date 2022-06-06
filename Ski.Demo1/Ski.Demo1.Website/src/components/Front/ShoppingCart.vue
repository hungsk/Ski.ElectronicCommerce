<template>
  <transition name="slide">
    <div class="rightSidebar" v-if="openSidebar">
      <div class="ps-4 pe-3 py-3">
        <a href="#" @click.prevent="closeSide">
          <i class="bi bi-arrow-right cart-arrow"></i>
        </a>
        <p class="text-center cart-title py-2 h5 fw-bold">
          購物車<i class="fas fa-shopping-cart ms-2"></i>
        </p>
        <div v-if="!$root.LoadingStatus && cart.length === 0 && cartVisible" class="text-center">
          購物車還沒有商品喔，<br>趕快新增一些吧!
        </div>
        <div v-if="!cartVisible" class="d-flex py-3 flex-column justify-content-center align-items-center">
          <p class="h5 py-3">結帳中...</p>
          <div class="three-cogs fa-2x pe-5">
            <i class="fa fa-cog fa-spin fa-2x"></i>
            <i class="fa fa-cog fa-spin fa-1x"></i>
            <i class="fa fa-cog fa-spin fa-1x"></i>
          </div>
        </div>
        <div class="d-flex justify-content-between align-items-center py-3" v-if="cart.length > 0 && cartVisible">
          <router-link class="btn btn-major" to="/checkoutConfirm" @click="openSidebar = false">
            結帳去<i class="fas fa-shopping-bag ms-2"></i>
          </router-link>
          <p class="mb-0"><strong>小計: {{ $filters.currency(total) }}</strong></p>
        </div>
        <div class="cart-content container-fluid" v-if="cartVisible">
          <template v-for="item in cart" :key="item.id">
            <div class="row py-3 border-bottom border-warning">
              <div class="col-4 mb-2">
                <img :src="item.product.imageUrl" :alt="item.product.title" class="img-side-cart">
              </div>
              <div class="col-8 mb-2 d-flex flex-column justify-content-between">
                <p class="fw-bold h6">{{ item.product.title }}</p>
                <span>{{ $filters.currency(item.total) }} </span>
              </div>
              <div class="col-9">
                <div class="input-number-group input-group">
                  <button type="button" class="btn btn-sm minus border border-2"
                  :disabled="loadingItem === item.id" @click="item.qty = minusQuantity(item.qty);changeProductQty(item)">
                    <i class="bi bi-dash-lg"></i>
                  </button>
                  <input type="number" v-model.number="item.qty" :disabled="loadingItem === item.id"
                  @change="item.qty = verifyNumber(item.qty);changeProductQty(item)"
                  class="text-center form-control input-number h-100 rounded-0">
                  <button type="button" class="btn btn-sm add border border-2" :disabled="loadingItem === item.id"
                  @click="item.qty = addQuantity(item.qty);changeProductQty(item)">
                    <i class="bi bi-plus-lg"></i>
                  </button>
                </div>
              </div>
              <div class="col-3 d-flex align-items-center">
                <a href="#" @click.prevent="deleteProduct(item)" :class="{'disabled-link':loadingItem === item.id}">
                  <i class="bi bi-x-square h4"></i>
                </a>
              </div>
            </div>
          </template>
        </div>
      </div>
    </div>
  </transition>
</template>

<script>
import { addQuantity, verifyNumber, minusQuantity, changeProductQty, deleteProduct, getCart } from '@/methods/cart.js'

export default {
  name: 'ShoppingCart',
  data () {
    return {
      openSidebar: false,
      loadingItem: '',
      cart: [],
      total: 0,
      final_total: 0,
      cartVisible: true
    }
  },
  methods: {
    addQuantity,
    minusQuantity,
    verifyNumber,
    changeProductQty,
    deleteProduct,
    getCart,
    closeSide () {
      this.openSidebar = false
    }
  },
  created () {
    this.emitter.on('openSidebar', (status) => {
      this.openSidebar = status
    })
    this.emitter.on('getCart', () => {
      this.getCart()
    })
    this.emitter.on('cartVisible', (status) => {
      this.cartVisible = status
    })
    this.getCart()
  }
}
</script>
