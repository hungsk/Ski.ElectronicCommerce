<template>
  <div class="container">
    <PageTitle :path="require('@/assets/images/page-title10.png')">結帳-訂單確認</PageTitle>
    <div class="row mh-wrapper">
      <div class="col-12 py-3">
        <div class="row justify-content-center">
          <div class="col-md-8">
            <CheckoutProgress :step = "1"></CheckoutProgress>
          </div>
        </div>
        <div class="row justify-content-center bg-light my-3">
          <div class="col-md-8">
            <div class="d-flex py-3 align-items-center justify-content-center">
              <img :src="require('@/assets/images/garland.png')" alt="開幕歡慶" width="100" class="img-fluid">
              <div class="px-5">
                <p class="h3">開幕歡慶</p>
                <p>輸入代號
                  <span class="text-success fw-bold">&nbsp;happybuy123&nbsp;</span>即享九折優惠
                </p>
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-md-12 p-3">
          <h3 class="py-1">購物清單</h3>
          <div class="line-info mb-3"></div>
            <div v-if="cart.length === 0 && !$root.LoadingStatus">
              <p class="h4 mb-0">購物車沒有商品了，趕快去逛逛!
                <router-link to="/" class="btn btn-outline-dark ms-3">回首頁</router-link>
              </p>
            </div>
            <div class="row py-3 align-items-center" v-for="item in cart" :key="item.id">
              <div class="col-4 col-md-2 mb-3">
                <img :src="item.product.imageUrl" :alt="item.product.title" class="img-checkout">
              </div>
              <div class="col-8 col-md-5 d-flex flex-column justify-content-between">
                <p class="h5">{{ item.product.title }}</p>
                <div class="input-number-group input-group w-75">
                  <button type="button" class="btn btn-sm minus border border-dark btn-outline-dark"
                  :disabled="loadingItem === item.id" @click="item.qty = minusQuantity(item.qty);changeProductQty(item)">
                    <i class="bi bi-dash-lg"></i>
                  </button>
                  <input type="number" v-model.number="item.qty" :disabled="loadingItem === item.id"
                  @change="item.qty = verifyNumber(item.qty);changeProductQty(item)"
                  class="text-center form-control input-number h-100 rounded-0 border-dark">
                  <button type="button" class="btn btn-sm add border border-dark btn-outline-dark"
                  :disabled="loadingItem === item.id" @click="item.qty = addQuantity(item.qty);changeProductQty(item)">
                    <i class="bi bi-plus-lg"></i>
                  </button>
                </div>
              </div>
              <div class="col-8 col-md-3">
                <p class="h5 my-1">
                  <span class="d-md-none d-inline-block">價格:</span>
                  {{ $filters.currency(item.total) }}
                </p>
              </div>
              <div class="col-4 col-md-2">
                <a href="#" @click.prevent="deleteProduct(item)" :class="{'disabled-link':loadingItem === item.id}">
                  <i class="bi bi-x-square h4"></i>
                </a>
              </div>
            </div>
            <hr v-if="cart.length > 0">
            <div class="row" v-if="cart.length > 0">
              <div class="col-12 text-end">
                <p class="h4 py-2">
                  <span class="me-3">總計:</span>{{ $filters.currency(total) }}
                </p>
                <p class="h4 py-2 text-success" v-if="total != final_total">
                  <span class="me-3">優惠價:</span>{{ $filters.currency(final_total) }}
                </p>
              </div>
            </div>
            <div class="row" v-if="cart.length > 0">
              <div class="col-md-8">
               <div class="input-group">
                <input type="text" name="coupon" id="coupon" class="form-control" v-model.trim="coupon_code">
                <button class="btn btn-major" @click="useCoupon">套用優惠卷</button>
               </div>
              </div>
            </div>
            <div class="row" v-if="cart.length > 0">
              <div class="col-12 py-3 text-end btn-lg">
                <button class="btn btn-major" type="button" @click="confirmOrder">
                  <span class="h5">下一步，填寫資料
                    <i class="fas fa-feather-alt ms-2"></i>
                  </span>
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import PageTitle from '@/components/Front/PageTitle.vue'
import CheckoutProgress from '@/components/Front/CheckoutProgress.vue'
import { getCart, addQuantity, verifyNumber, minusQuantity, changeProductQty, deleteProduct } from '@/methods/cart.js'

export default {
  name: 'CheckoutConfirm',
  components: {
    PageTitle,
    CheckoutProgress
  },
  data () {
    return {
      cart: [],
      total: 0,
      final_total: 0,
      loadingItem: '',
      coupon_code: ''
    }
  },
  methods: {
    getCart,
    addQuantity,
    verifyNumber,
    minusQuantity,
    changeProductQty,
    deleteProduct,
    useCoupon () {
      const vm = this
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/coupon`
      const coupon = {
        code: vm.coupon_code
      }
      vm.emitter.emit('loading', true)
      vm.$http.post(api, { data: coupon }).then((res) => {
        if (res.data.success) {
          vm.emitter.emit('push-message', {
            style: 'success',
            title: '訊息通知',
            content: res.data.message,
            icon: 'fas fa-check-circle'
          })
          vm.getCart()
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知',
            content: res.data.message,
            icon: 'fas fa-exclamation-circle'
          })
          vm.emitter.emit('loading', false)
        }
      }).catch((err) => {
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知',
          content: err,
          icon: 'fas fa-exclamation-circle'
        })
        vm.emitter.emit('loading', false)
      })
    },
    confirmOrder () {
      const vm = this
      vm.$swal({
        title: '確認送出?',
        text: '送出後即無法更改訂單內容',
        showCancelButton: true,
        confirmButtonText: '確認送出！',
        cancelButtonText: '取消',
        icon: 'info'
      }).then((handle) => {
        if (handle.isConfirmed) {
          vm.$router.push('/checkoutInfo')
        }
      })
    }
  },
  created () {
    this.emitter.emit('cartVisible', false)
    this.emitter.on('RefreshCart', () => {
      this.getCart()
    })
    this.getCart()
  }
}
</script>
