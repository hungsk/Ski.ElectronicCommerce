<template>
  <div class="modal fade" ref="modal" id="couponModal" tabindex="-1" aria-labelledby="couponModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-header bg-primary text-white">
          <h5 class="modal-title" id="couponModalLabel">
            <slot name="title"></slot>
          </h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="row">
            <div class="col-12">
              <div class="mb-3">
                <label for="title" class="form-label">*名稱</label>
                <input type="text" class="form-control" id="title" v-model="currentCoupon.title"
                placeholder="請輸入名稱">
              </div>
              <div class="mb-3">
                <label for="percent" class="form-label">*優惠%</label>
                <input type="text" class="form-control" id="percent" v-model.number="currentCoupon.percent" placeholder="請輸入優惠%">
              </div>
              <div class="mb-3">
                <label for="code" class="form-label">*優惠碼</label>
                <input type="text" class="form-control" id="code" v-model="currentCoupon.code" placeholder="請輸入優惠碼">
              </div>
              <div class="mb-3">
                <label for="due_date" class="form-label">*到期日</label>
                <input type="date" class="form-control" id="due_date" v-model="currentCoupon.due_date"
                placeholder="請選擇到期日">
              </div>
              <div class="mb-3">
                <div class="form-check">
                  <input class="form-check-input" type="checkbox"
                  :true-value="1" :false-value="0" id="is_enabled" v-model="currentCoupon.is_enabled">
                  <label class="form-check-label" for="is_enabled">
                  *是否啟用
                  </label>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-outline-dark" data-bs-dismiss="modal">取消</button>
          <button type="button" class="btn btn-primary btn-hv-style" @click="updateCoupon(currentCoupon)">儲存</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import modalMixin from '@/mixin/ModalMixin.js'

export default {
  name: 'CouponMainModal',
  mixins: [modalMixin],
  props: {
    currentItem: {
      type: Object,
      default () { return {} }
    }
  },
  data () {
    return {
      modal: {},
      currentCoupon: {}
    }
  },
  watch: {
    currentItem () {
      this.currentCoupon = this.currentItem
    }
  },
  methods: {
    updateCoupon (currentCoupon) {
      this.$emit('updateCoupon', currentCoupon)
    }
  }
}
</script>
