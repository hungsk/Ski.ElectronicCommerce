<template>
  <div class="container">
    <div class="row">
      <div class="col-12 py-5 text-center">
        <h1>優惠卷管理</h1>
      </div>
      <div class="col-12 text-end py-3">
        <button type="button" class="btn btn-primary btn-hv-style" @click="openModal(true)">
          新增優惠卷
        </button>
      </div>
      <div class="col-12">
        <div class="table-responsive">
          <table class="table" id="table-product" v-if="couponAll.length>0">
            <thead>
              <tr>
                <th>名稱</th>
                <th>優惠%</th>
                <th>是否啟用</th>
                <th>到期日</th>
                <th>編輯</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in couponAll" :key="item.id">
                <td>{{ item.title }}</td>
                <td>{{ item.percent }}</td>
                <td>{{ item.is_enabled ? '啟用' : '未啟用' }}</td>
                <td>{{ $filters.date(item.due_date) }}</td>
                <td>
                  <button type="button" class="btn btn-outline-primary me-2"
                  @click="openModal(false, item)">修改</button>
                  <button type="button" class="btn btn-outline-dark"
                  @click="openDeleteModal(item)">刪除</button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      <div class="col-12">
        <BackPagination :pageItem="pageItem" @updatePage="getCoupon"/>
      </div>
    </div>
    <MainModal ref="modalMain" :currentItem="currentCoupon"
    @updateCoupon="updateCoupon">
      <template v-slot:title>{{ modalTitle }}</template>
    </MainModal>
    <DeleteModal ref="modalDelete" :currentItem="currentCoupon"
    @deleteItem="deleteCoupon">
      <template v-slot:title>{{ modalTitle }}</template>
    </DeleteModal>
  </div>
</template>

<script>
import MainModal from '@/components/Back/CouponMainModal.vue'
import DeleteModal from '@/components/Back/DeleteModal.vue'
import BackPagination from '@/components/Back/BackPagination.vue'

export default {
  name: 'CouponManage',
  components: {
    MainModal,
    DeleteModal,
    BackPagination
  },
  data () {
    return {
      couponAll: [],
      modalTitle: '',
      isNew: true,
      currentCoupon: {},
      pageItem: {}
    }
  },
  methods: {
    getCoupon (page = 1) {
      const vm = this
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/coupons?page=${page}`
      vm.emitter.emit('loading', true)
      vm.$http.get(api).then((res) => {
        if (res.data.success) {
          vm.couponAll = [...res.data.coupons]
          vm.pageItem = { ...res.data.pagination }
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知',
            content: res.data.message,
            icon: 'fas fa-exclamation-circle'
          })
        }
        vm.emitter.emit('loading', false)
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
    openModal (isNew, item) {
      if (isNew) {
        this.modalTitle = '新增優惠卷'
        this.currentCoupon = {}
        this.currentCoupon.is_enabled = 0
      } else {
        this.modalTitle = '修改優惠卷'
        const date = new Date(item.due_date * 1000)
        let month = '' + (date.getMonth() + 1)
        let day = '' + date.getDate()
        if (month.length < 2) {
          month = '0' + month
        }
        if (day.length < 2) {
          day = '0' + day
        }
        const dateFormat = `${date.getFullYear()}-${month}-${day}`
        this.currentCoupon = { ...item, due_date: dateFormat }
      }
      this.isNew = isNew
      this.$refs.modalMain.showModal()
    },
    openDeleteModal (item) {
      this.modalTitle = '刪除商品'
      this.currentCoupon = { ...item }
      this.$refs.modalDelete.showModal()
    },
    updateCoupon (item) {
      const vm = this
      let api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/coupon`
      let httpMethod = 'post'
      if (!this.isNew) {
        api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/coupon/${item.id}`
        httpMethod = 'put'
      }
      const coupon = { ...item, due_date: Math.floor(new Date(item.due_date) / 1000) }
      vm.emitter.emit('loading', true)
      vm.$refs.modalMain.hideModal()
      vm.$http[httpMethod](api, { data: coupon }).then((res) => {
        if (res.data.success) {
          vm.emitter.emit('push-message', {
            style: 'success',
            title: '訊息通知',
            content: '優惠卷更新成功',
            icon: 'fas fa-check-circle'
          })
          vm.getCoupon(vm.pageItem.currentPage)
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知',
            content: res.data.message.join('、'),
            icon: 'fas fa-exclamation-circle'
          })
        }
        vm.emitter.emit('loading', false)
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
    deleteCoupon (id) {
      const vm = this
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/coupon/${id}`
      vm.$http.delete(api).then((res) => {
        if (res.data.success) {
          vm.$refs.modalDelete.hideModal()
          vm.emitter.emit('push-message', {
            style: 'success',
            title: '訊息通知',
            content: '優惠卷刪除成功',
            icon: 'fas fa-check-circle'
          })
          vm.getCoupon(vm.pageItem.currentPage)
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知',
            content: res.data.message,
            icon: 'fas fa-exclamation-circle'
          })
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
    }
  },
  created () {
    this.getCoupon()
  }
}
</script>
