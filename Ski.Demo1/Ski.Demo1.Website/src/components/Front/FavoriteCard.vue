<template>
  <router-link :to="`/product/${data.id}`">
    <div class="card shadow">
      <div class="card-img-wrapper">
        <img :src="data.imageUrl" class="card-img-top img-card" :alt="data.title">
        <div class="card-img-layer d-flex justify-content-center align-items-center">
        <p class="h5">點選查看</p></div>
      </div>
      <div class="card-body">
        <p class="card-title h5 mb-1">{{ data.title }}</p>
        <div class="d-flex justify-content-between align-items-center pb-1">
          <span class="card-text card-gray-text">
            <small>{{ data.description }}</small>
          </span>
          <span class="badge rounded-pill bg-success"
          v-if="data.price != data.origin_price">特價</span>
        </div>
        <div class="d-flex justify-content-between align-items-center pb-1">
          <span class="card-text text-danger">
            <strong>{{ $filters.currency(data.price) }}</strong>
          </span>
          <span class="card-text" v-if="data.price != data.origin_price">
            <del>{{ $filters.currency(data.origin_price) }}</del>
          </span>
        </div>
        <div class="d-flex justify-content-between py-1 flex-wrap">
          <button type="button" class="btn btn-outline-dark mb-2"
          @click.prevent="removeFavorite">
            <i class="fas fa-trash-alt me-2"></i>取消收藏
          </button>
          <button type="button" class="btn btn-major mb-2"
          @click.prevent="addToCart(data)">
            <i class="fas fa-shopping-cart fa-lg me-2"></i>加入購物車
          </button>
        </div>
      </div>
    </div>
  </router-link>
</template>

<script>
export default {
  name: 'FavoriteCard',
  props: {
    data: {
      type: Object
    }
  },
  methods: {
    removeFavorite () {
      this.$emit('remove', this.data.id)
    },
    addToCart (item) {
      const vm = this
      const token = document.cookie.replace(/(?:(?:^|.*;\s*)hexToken\s*=\s*([^;]*).*$)|^.*$/, '$1')
      vm.$http.defaults.headers.common.Authorization = `Bearer ${token}`
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/cart`
      const data = { product_id: item.id, qty: 1 }
      vm.emitter.emit('loading', true)
      vm.$http.post(api, { data }).then((res) => {
        if (res.data.success) {
          vm.emitter.emit('push-message', {
            style: 'success',
            title: '訊息通知',
            content: res.data.message,
            icon: 'fas fa-check-circle'
          })
          vm.emitter.emit('getCart')
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知70',
            content: res.data.message.join('、'),
            icon: 'fas fa-exclamation-circle'
          })
        }
        vm.emitter.emit('loading', false)
      }).catch((err) => {
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知79',
          content: err,
          icon: 'fas fa-exclamation-circle'
        })
        vm.emitter.emit('loading', false)
      })
    }
  }
}
</script>
