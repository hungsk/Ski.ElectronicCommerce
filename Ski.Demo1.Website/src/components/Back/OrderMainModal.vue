<template>
  <div class="modal fade" ref="modal" id="OrderModal" tabindex="-1"
  aria-labelledby="OrderModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-fullscreen">
      <div class="modal-content">
        <div class="modal-header bg-primary text-white">
          <h5 class="modal-title" id="OrderModalLabel">
            <slot name="title"></slot>
          </h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal"
          aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="row">
            <div class="mb-3 col-md-6">
              <label  class="form-label">訂單編號</label>
              <p class="h5">{{ currentOrder.id }}</p>
            </div>
            <div class="mb-3 col-md-6">
              <label for="create_at" class="form-label">建立時間</label>
              <input type="hidden" name="create_at" v-model="currentOrder.create_at">
              <p class="h5">{{ $filters.date(currentOrder.create_at) }}</p>
            </div>
            <div class="mb-3 col-md-6">
              <label for="is_paid" class="form-label">*付款狀態</label>
              <select name="is_paid" id="is_paid" class="form-select"
              v-model="currentOrder.is_paid">
                <option value="true">已付款</option>
                <option value="false">未付款</option>
              </select>
            </div>
            <div class="mb-3 col-md-6">
              <label for="total" class="form-label">*總金額</label>
              <input type="hidden" id="total" v-model.number="currentOrder.total">
              <p class="h5">{{ $filters.currency(currentOrder.total)}}</p>
            </div>
            <div class="my-3 col-12">
              <label class="form-label">*訂購商品</label>
              <div class="bg-light row p-md-3">
                <template v-for="item in currentOrder.products" :key="item.id">
                  <div class="col-md-5 col-sm-6 mb-2 col-12">
                    <input type="text" name="title" disabled v-model="item.product.title"
                    class="form-control">
                  </div>
                  <div class="col-md-3 col-sm-3 mb-2 col-6">
                    <div class="input-number-group input-group">
                      <button type="button" class="btn btn-sm minus border border-2"
                      @click="item.qty = minusQuantity(item.qty);changeSum(item)">
                        <i class="bi bi-dash-lg"></i>
                      </button>
                      <input type="number" v-model.number="item.qty"
                      @change="item.qty = verifyNumber(item.qty);changeSum(item)"
                      class="text-center form-control input-number h-100 rounded-0">
                      <button type="button" class="btn btn-sm add border border-2"
                      @click="item.qty = addQuantity(item.qty);changeSum(item)">
                        <i class="bi bi-plus-lg"></i>
                      </button>
                    </div>
                  </div>
                  <div class="col-md-4 col-sm-3 mb-2 col-6">
                    <input type="text" disabled name="final_total" v-model="item.final_total"
                    class="form-control">
                  </div>
                </template>
              </div>
            </div>
            <div class="col-12">
              <label class="form-label">訂購者資料</label>
            </div>
            <div class="mb-3 col-md-6">
              <label for="username" class="form-label">*姓名:</label>
              <input type="text" name="username" id="username" class="form-control"
              v-model="currentOrder.user.name">
            </div>
            <div class="mb-3 col-md-6">
              <label for="tel" class="form-label">*電話:</label>
              <input type="text" name="tel" id="tel" class="form-control"
              v-model="currentOrder.user.tel">
            </div>
            <div class="mb-3 col-md-6">
              <label for="email" class="form-label">*email:</label>
              <input type="email" name="email" id="email" class="form-control"
              v-model="currentOrder.user.email">
            </div>
            <div class="mb-3 col-6">
              <label for="address" class="form-label">*地址:</label>
              <input type="text" name="address" id="address" class="form-control"
              v-model="currentOrder.user.address">
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-outline-dark" data-bs-dismiss="modal">
            取消
          </button>
          <button type="button" class="btn btn-primary btn-hv-style"
          @click="updateOrder(currentOrder)">
            儲存
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ModalMixin from '@/mixin/ModalMixin.js'
import { addQuantity, minusQuantity, verifyNumber } from '@/methods/cart.js'

export default {
  name: 'OrderMainModal',
  mixins: [ModalMixin],
  props: {
    currentItem: {
      type: Object
    }
  },
  data () {
    return {
      currentOrder: {
        user: {},
        products: {}
      }
    }
  },
  watch: {
    currentItem () {
      this.currentOrder = this.currentItem
    }
  },
  methods: {
    addQuantity,
    minusQuantity,
    verifyNumber,
    changeSum (product) {
      console.log('changeSum')
      console.log(product)
      product.final_total = product.qty * ((product.coupon.percent === 0 ? 100 : product.coupon.percent) / 100) * product.total
      this.currentOrder.total = 0
      for (const item in this.currentOrder.products) {
        this.currentOrder.total += this.currentOrder.products[item].final_total
      }
    },
    updateOrder () {
      console.log('updateOrder')
      this.$emit('updateOrder', this.currentOrder)
    }
  }
}
</script>
