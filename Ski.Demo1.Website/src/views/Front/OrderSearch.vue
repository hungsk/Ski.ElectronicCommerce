<template>
  <div class="container">
    <PageTitle :path="require('@/assets/images/page-title13.png')">訂單查詢</PageTitle>
    <div class="row mh-wrapper">
      <div class="col-12">
        <div class="container bg-light" :class="{'h-100': !isSearch}">
          <div class="row">
            <div class="col-12 pt-4">
              <router-link to="/">
                <p class="h1">
                  <i class="bi bi-arrow-left me-3"></i>
                </p>
              </router-link>
            </div>
          </div>
          <div class="row justify-content-center p-2 p-md-4">
            <div class="col-md-9 col-lg-8 d-flex align-items-center py-4 justify-content-center">
              <div class="me-5 d-none d-sm-block">
                <img :src="require('@/assets/images/packing-list.png')" width="128" alt="訂單查詢">
              </div>
              <div class="d-flex flex-column justify-content-between h-75 w-100">
                <label for="search" class="form-label h3">輸入訂單號碼查詢訂單</label>
                <div class="input-group">
                  <input type="text" name="search" id="search" class="form-control"
                  v-model.trim="orderId" ref="search">
                  <button type="button" class="btn btn-major" @click="searchOrder">
                    <i class="fas fa-search"></i>
                  </button>
                </div>
              </div>
            </div>
          </div>
          <div class="text-center pb-5" v-if="!isSearch">
            <i class="fas fa-search fa-10x text-warning"></i>
          </div>
        </div>
        <div class="row py-3" v-if="isSearch">
          <div class="col-12 my-3">
            <h3>
              <i class="fas fa-file-invoice-dollar me-2"></i>訂單狀態
            </h3>
            <div class="line-info mb-3"></div>
            <p>付款狀態:
              <span class="text-success ms-2" v-if="order.is_paid">已付款</span>
              <span class="ms-2" v-if="!order.is_paid">未付款</span>
            </p>
          </div>
          <div class="col-12 my-3">
            <h3>
              <i class="fas fa-shopping-cart me-2"></i>購物清單
            </h3>
            <div class="line-info mb-3"></div>
            <div class="p-md-5 p-3">
              <div class="row border-bottom border-dark">
                <div class="col-md-6">
                  <p class="h5" v-if="order.total">
                    總計: {{ $filters.currency(order.total) }}
                  </p>
                </div>
              </div>
              <div class="row py-3 align-items-center border-bottom border-dark"
              v-for="item in order.products" :key="item.id">
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
          <div class="col-12 my-3">
            <h3>
              <i class="fas fa-shipping-fast me-2"></i>收件資訊
            </h3>
            <div class="line-info mb-3"></div>
            <div class="d-flex row">
              <div class="col-md-7 bg-secondary d-flex align-items-center">
                <div class="box-wrapper p-3 p-md-5 m-3">
                  <div class="row">
                    <div class="col-12">
                      <p>
                        <strong class="me-3">訂購人:</strong>{{ order.user.name }}
                      </p>
                    </div>
                    <div class="col-12">
                      <p>
                        <strong class="me-3">email:</strong>{{ order.user.email }}
                      </p>
                    </div>
                    <div class="col-12">
                      <p>
                        <strong class="me-3">電話:</strong>{{ order.user.tel }}
                      </p>
                    </div>
                    <div class="col-12">
                      <p>
                        <strong class="me-3">地址:</strong>{{ order.user.address }}
                      </p>
                    </div>
                    <div class="col-12">
                      <p>
                        <strong class="me-3">留言:</strong>{{ order.message }}
                      </p>
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-md-5 d-none d-md-block p-md-5 p-3">
                <img :src="require('@/assets/images/delivery-truck.png')" alt="貨車" width="256" height="256">
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

export default {
  name: 'OrderSearch',
  components: {
    PageTitle
  },
  data () {
    return {
      isSearch: false,
      orderId: '',
      order: {}
    }
  },
  methods: {
    searchOrder () {
      const vm = this
      if (vm.orderId === '') return
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/order/${vm.orderId}`
      vm.emitter.emit('loading', true)
      vm.$http.get(api).then((res) => {
        if (res.data.success) {
          vm.$refs.search.value = ''
          if (res.data.order === null) {
            vm.isSearch = false
            vm.emitter.emit('push-message', {
              style: 'danger',
              title: '錯誤通知',
              content: '找不到此訂單',
              icon: 'fas fa-exclamation-circle'
            })
          } else {
            vm.order = JSON.parse(JSON.stringify(res.data.order))
            vm.isSearch = true
          }
        } else {
          vm.isSearch = false
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知',
            content: res.data.message,
            icon: 'fas fa-exclamation-circle'
          })
        }
        vm.emitter.emit('loading', false)
      }).catch((err) => {
        vm.isSearch = false
        vm.emitter.emit('loading', false)
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知',
          content: err,
          icon: 'fas fa-exclamation-circle'
        })
      })
    }
  },
  created () {
    this.emitter.emit('cartVisible', true)
  }
}
</script>
