<template>
  <div class="container">
    <PageTitle :path="require('@/assets/images/page-title7.png')">商品收藏</PageTitle>
    <div class="mh-wrapper">
      <div class="row">
        <div class="pt-4 col-12">
          <router-link to="/">
            <p class="h1">
              <i class="bi bi-arrow-left me-3"></i>
            </p>
          </router-link>
        </div>
      </div>
      <div class="row py-4" v-masonry transition-duration="0.3s" item-selector=".grid-item">
        <div class="col-md-6 col-lg-4 col-xxl-3 mb-3 grid-item" v-masonry-tile
        v-for="item in favoriteList" :key="item.id">
          <FavoriteCard :data="item" @remove="removeFavorite"/>
        </div>
        <div v-if="favoriteList.length === 0 && !$root.LoadingStatus" class="py-3">
          <p class="h4 text-center">暫時沒有收藏項目喔，來新增一些吧</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import PageTitle from '@/components/Front/PageTitle.vue'
import FavoriteCard from '@/components/Front/FavoriteCard.vue'

export default {
  name: 'Favorite',
  components: {
    PageTitle,
    FavoriteCard
  },
  data () {
    return {
      favoriteIdList: [],
      favoriteList: [],
      productAll: [],
      listIsLoading: false
    }
  },
  methods: {
    getFavoriteList () {
      this.favoriteIdList = JSON.parse(window.localStorage.getItem('favorite')) || []
      this.favoriteList = []
      this.favoriteIdList.forEach((item) => {
        const match = this.productAll.filter((product) => product.id === item)
        if (match) {
          this.favoriteList.push(...match)
        }
      })
    },
    getProductAll () {
      const vm = this
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/products/all`
      vm.emitter.emit('loading', true)
      vm.$http.get(api).then((res) => {
        if (res.data.success) {
          vm.productAll = res.data.products
          vm.getFavoriteList()
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
    removeFavorite (id) {
      const index = this.favoriteIdList.findIndex((item) => item === id)
      this.favoriteIdList.splice(index, 1)
      window.localStorage.setItem('favorite', JSON.stringify(this.favoriteIdList))
      this.emitter.emit('push-message', {
        style: 'success',
        title: '訊息通知',
        content: '已取消收藏',
        icon: 'fas fa-check-circle'
      })
      this.getFavoriteList()
    }
  },
  created () {
    this.emitter.emit('cartVisible', true)
    this.getProductAll()
  }
}
</script>
