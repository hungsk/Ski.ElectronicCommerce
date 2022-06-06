<template>
  <div class="container">
    <PageTitle :path="require('@/assets/images/page-title12.png')">結帳-結帳付款</PageTitle>
    <div class="row mh-wrapper">
      <div class="col-12 py-3">
        <div class="row justify-content-center">
          <div class="col-md-8">
            <CheckoutProgress :step = "3"></CheckoutProgress>
          </div>
        </div>
        <div class="bg-light row justify-content-center p-4 my-4" v-if="order.is_paid" ref="orderSearch">
          <div class="col-md-8 d-flex flex-column flex-md-row justify-content-center align-items-center">
            <div class="me-0 me-md-5 mb-4 mb-md-0">
              <img :src="require('@/assets/images/packing-list.png')" alt="訂單查詢" height="128">
            </div>
            <div>
              <p>訂單查詢號碼，可至<router-link to="/orderSearch" class="text-primary mx-2 fw-bold">訂單查詢</router-link>查詢訂單紀錄</p>
              <div class="input-group">
                <input id="copy" :value="orderId" class="form-control input-copy" />
                <button type="button" class="btn btn-major btn-copy" data-clipboard-target="#copy">複製訂單編號</button>
              </div>
            </div>
          </div>
        </div>
        <div>
          <h3>購物清單</h3>
          <div class="line-info mb-3"></div>
          <div class="p-md-5 p-3">
            <div class="row border-bottom border-dark">
              <div class="col-md-6">
                <p class="h5" v-if="order.total">總計: {{ $filters.currency(order.total) }}</p>
              </div>
              <div class="col-md-6 text-md-end">
                <p class="h5">狀態:
                  <span class="text-success" v-if="order.is_paid">已付款</span>
                  <span v-if="!order.is_paid">尚未付款</span>
                </p>
              </div>
            </div>
            <div class="row py-3 align-items-center border-bottom border-dark" v-for="item in order.products" :key="item.id">
              <div class="col-md-2 col-5">
                <img :src="item.product.imageUrl" :alt="item.product.title" class="img-checkout">
              </div>
              <div class="col-md-5 col-7">
                <p class="fw-bold h5">{{ item.product.title }}</p>
                <span class="d-md-none me-3">數量: {{ item.qty }}</span>
                <span class="d-md-none">{{ $filters.currency(item.final_total) }}</span>
              </div>
              <div class="col-md-2 d-none d-md-block">
                <p>數量: {{ item.qty }}</p>
              </div>
              <div class="col-md-3 d-none d-md-block">
                <p>{{ $filters.currency(item.final_total) }}</p>
              </div>
            </div>
          </div>
        </div>
        <div>
          <h3>聯絡資料</h3>
          <div class="line-info mb-3"></div>
          <div class="bg-light p-md-5 p-3 d-flex row">
            <div class="col-md-7">
              <div class="row">
                <div class="col-12">
                  <p>
                    <strong class="me-3">訂購人:</strong>{{ order.user.name }}
                  </p>
                </div>
                <div class="col-12">
                  <p>
                    <strong class="me-3">email:&nbsp;</strong>{{ order.user.email }}
                  </p>
                </div>
                <div class="col-12">
                  <p>
                    <strong class="me-3">電話:&nbsp;&nbsp;</strong>{{ order.user.tel }}
                  </p>
                </div>
                <div class="col-12">
                  <p>
                    <strong class="me-3">地址:&nbsp;&nbsp;</strong>{{ order.user.address }}
                  </p>
                </div>
                <div class="col-12">
                  <p>
                    <strong class="me-3">留言:&nbsp;&nbsp;</strong>{{ order.message }}
                  </p>
                </div>
              </div>
            </div>
            <div class="col-md-5 d-none d-md-block">
              <img :src="require('@/assets/images/delivery-truck.png')" alt="貨車" width="256" height="256">
            </div>
          </div>
        </div>
        <div class="text-end py-3" v-if="!order.is_paid">
          <button type="button" class="btn btn-lg btn-major" @click="payOrder">
            確認無誤，結帳付款<i class="fas fa-file-invoice-dollar ms-2"></i>
          </button>
        </div>
        <div class="text-center py-3" v-if="order.is_paid">
          <router-link to="/" class="btn btn-lg btn-major" >
            回首頁<i class="fas fa-home ms-2"></i>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import PageTitle from '@/components/Front/PageTitle.vue'
import CheckoutProgress from '@/components/Front/CheckoutProgress.vue'
import ClipboardJS from 'clipboard'

export default {
  name: 'CheckoutPay',
  components: {
    PageTitle,
    CheckoutProgress
  },
  data () {
    return {
      orderId: '',
      order: {
        user: {},
        products: {}
      },
      clipboard: null
    }
  },
  methods: {
    getOrder () {
      console.log('getOrder')
      const vm = this
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/order/${vm.orderId}`
      console.log(api)
      vm.emitter.emit('loading', true)
      vm.$http.get(api).then((res) => {
        if (res.data.success) {
          vm.order = JSON.parse(JSON.stringify(res.data.order))
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
    payOrder () {
      const vm = this
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/pay/${vm.orderId}`
      vm.emitter.emit('loading', true)
      vm.$http.post(api).then((res) => {
        if (res.data.success) {
          vm.getOrder()
          vm.$swal({
            title: '付款成功',
            html: `訂購完成，感謝您的購買!<br>訂單查詢編號:<span class="text-success">${vm.orderId}</span>，可至訂單查詢查看訂單紀錄`,
            confirmButtonText: '關閉',
            icon: 'success'
          }).then(() => {
            const searchPos = this.$refs.orderSearch.offsetTop
            window.scroll(0, searchPos - 200)
          })
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
    }
  },
  created () {
    this.emitter.emit('cartVisible', false)
    this.orderId = this.$route.params.id
    this.getOrder()
  },
  mounted () {
    this.clipboard = new ClipboardJS('.btn-copy')
  }
}
</script>
