<template>
  <div class="container">
    <div class="row mh-wrapper">
      <div class="col-12 py-3">
        <Menu/>
      </div>
      <div class="col-12 py-3">
        <a href="#" @click.prevent="historyBack">
          <i class="bi bi-arrow-left h1"></i>
        </a>
      </div>
      <div class="col-md-5 py-3">
        <img :src="currentProduct.imageUrl" :alt="currentProduct.title" class="detail-img">
      </div>
      <div class="col-md-7 py-3 ps-md-3 d-flex flex-column justify-content-between" v-cloak>
        <p class="h5 py-1">
          <span class="badge bg-dark rounded-pill">{{ currentProduct.category }}</span>
        </p>
        <h1 class="h3 py-1">{{ currentProduct.title }}</h1>
        <div class="title-line mb-3"></div>
        <p class="text-card-gray-text">型號: {{ currentProduct.description }}</p>
        <p class="d-flex align-items-center">
          <span v-if="currentProduct.origin_price != currentProduct.price"
          class="me-2 badge rounded-pill bg-success">
            特價
          </span>
          <del class="me-5" v-if="currentProduct.origin_price != currentProduct.price">
            {{ $filters.currency(currentProduct.origin_price) }}
          </del>
          <span class="h4 text-danger" v-if="currentProduct.price">
            {{ $filters.currency(currentProduct.price) }}/{{ currentProduct.unit }}
          </span>
        </p>
        <div class="input-number-group input-group">
          <button type="button" @click="currentProduct.num = minusQuantity(currentProduct.num)"
          class="btn minus border border-2 border-dark btn-outline-dark"><i class="bi bi-dash-lg"></i>
          </button>
          <input type="number" v-model.number="currentProduct.num" class="text-center input-number h-100 rounded-0 detail-input-number"
          @change="currentProduct.num = verifyNumber(currentProduct.num)">
          <button type="button" @click="currentProduct.num = addQuantity(currentProduct.num)"
          class="btn add border border-2 border-dark btn-outline-dark"><i class="bi bi-plus-lg"></i>
          </button>
        </div>
        <div class="pt-3 detail-btn-group">
          <button type="button" v-if="isFavorite" @click="cancelFavorite" class="btn btn-outline-dark detail-btn">
            <i class="fas fa-heart fa-lg me-2"></i>取消收藏
          </button>
          <button type="button" v-else @click="saveFavorite" class="btn btn-outline-dark detail-btn">
            <i class="fas fa-heart fa-lg me-2"></i>加入收藏
          </button>
          <button type="button" @click="addToCart(currentProduct)" class="btn btn-major detail-btn">
            <i class="fas fa-shopping-cart fa-lg me-2"></i>加入購物車
          </button>
        </div>
      </div>
      <div class="col-12 py-4">
        <div class="row">
          <div class="col-md-7 col-lg-9">
            <h3>
              <i class="fas fa-info-circle me-2"></i>產品資訊
            </h3>
            <div class="line-info mb-3"></div>
            <p>
              <pre class="fs-6">{{ currentProduct.content }}</pre>
            </p>
          </div>
          <div class="col-md-5 col-lg-3 py-3 d-flex align-items-center">
            <div class="detail-info-box px-3 py-3 w-100 d-flex align-items-center fw-bold">
              <div class="pe-3">
                <i class="bi bi-telephone-fill h1"></i>
              </div>
              <div>
                <p class="mb-1">更多詳細資訊</p>
                <p class="mb-1">請電聯客服專員</p>
                <p class="mb-0 fs-4"><a href="tel:+0223456789">02-2345-6789</a></p>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-12 py-4">
       <div class="row justify-content-center">
          <swiper @swiper="setThumbsSwiper" :slidesPerView="5" :direction="'vertical'"
          :freeMode="true" :watchSlidesVisibility="true" :watchSlidesProgress="true"
          :allowTouchMove="false" class="swiper-item py-2 col-md-2 col-lg-1 d-none d-md-flex">
            <template v-for="item in currentProduct.imagesUrl" :key="item">
              <swiper-slide v-if="item">
                <img :src="item" class="detail-display-imgitem" />
              </swiper-slide>
            </template>
          </swiper>
          <swiper :style="{'--swiper-navigation-color': 'rgba(255,255,255,0.6)'}" class="swiper-detail col-md-8 col-lg-6"
          :loop="true" :spaceBetween="10" :navigation="true" :thumbs="{ swiper: thumbsSwiper }">
            <template v-for="item in currentProduct.imagesUrl" :key="item">
              <swiper-slide v-if="item">
                <img :src="item" class="detail-display-img"/>
              </swiper-slide>
            </template>
          </swiper>
        </div>
      </div>
      <div class="col-12 py-4">
        <h3>
          <i class="fas fa-bullhorn me-2"></i>購物須知
        </h3>
        <div class="line-info mb-3"></div>
        <p>
          訂單成立後，將安排時間到現場，確認現場人員下料，並於檢查後簽收。
        </p>
      </div>
      <div class="col-12 py-4">
        <h3>
          <i class="fas fa-boxes me-2"></i>類似商品
        </h3>
        <div class="line-info mb-3"></div>
          <swiper :slidesPerView="4" :spaceBetween="20" class="swiper-container-index"
          :slidesPerGroup="4" :navigation="true" :lazy="true" :loop="true"
          :breakpoints="swiper.breakpoints">
            <swiper-slide v-for="item in productFilter" :key="item.id">
              <router-link :to="`/product/${item.id}`">
                <img :src="item.imageUrl" class="swiper-lazy swiper-main-img"/>
                <img :src="item.imageUrl" class="swiper-lazy swiper-second-img "/>
                <div class="swiper-lazy-preloader swiper-lazy-preloader-white"></div>
                <div class="view-more">
                  <span class="h5">查看更多</span>
                </div>
                <div class="slide-captions">
                  <p class="text-start mb-0">{{ item.title }}</p>
                  <p class="text-start mb-0"><small>{{ item.description }}</small></p>
                </div>
              </router-link>
            </swiper-slide>
          </swiper>
        </div>
    </div>
  </div>
</template>

<script>
import Menu from '@/components/Front/Menu.vue'
import { addQuantity, minusQuantity, verifyNumber } from '@/methods/cart.js'
import SwiperCore, { Navigation, Thumbs } from 'swiper'
import { Swiper, SwiperSlide } from 'swiper/vue'
import 'swiper/swiper.scss'
import 'swiper/components/navigation/navigation.scss'
import 'swiper/components/thumbs/thumbs.min.css'
SwiperCore.use([Navigation, Thumbs])

export default {
  name: 'ProductDetail',
  components: {
    Swiper,
    SwiperSlide,
    Menu
  },
  data () {
    return {
      productId: '',
      currentProduct: {},
      productAll: [],
      productFilter: [],
      thumbsSwiper: null,
      isFavorite: false,
      swiper: {
        breakpoints: {
          0: {
            slidesPerView: 1,
            spaceBetween: 10,
            slidesPerGroup: 1
          },
          567: {
            slidesPerView: 1,
            spaceBetween: 10,
            slidesPerGroup: 1
          },
          768: {
            slidesPerView: 3,
            spaceBetween: 20,
            slidesPerGroup: 3
          },
          1024: {
            slidesPerView: 4,
            spaceBetween: 20,
            slidesPerGroup: 4
          }
        }
      }
    }
  },
  watch: {
    $route (to, from) {
      if (to.name === 'ProductDetail') {
        this.fetchInfo()
      }
    }
  },
  methods: {
    addQuantity,
    minusQuantity,
    verifyNumber,
    historyBack () {
      const { category, categoryMain, page } = this.$route.query
      if (!category || !categoryMain) {
        this.$router.go(-1)
      } else {
        this.$router.push({ path: `/product/${categoryMain}/${category}`, query: { page: page } })
      }
    },
    getProductDetail () {
      return new Promise((resolve, reject) => {
        const vm = this
        const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/product/${vm.productId}`
        vm.emitter.emit('loading', true)
        vm.$http.get(api).then((res) => {
          if (res.data.success) {
            resolve({ ...res.data.product })
          } else {
            reject(res.data.message)
          }
          vm.emitter.emit('loading', false)
        }).catch((err) => {
          reject(err)
          vm.emitter.emit('loading', false)
        })
      })
    },
    getProductAll () {
      return new Promise((resolve, reject) => {
        const vm = this
        const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/products/all`
        vm.emitter.emit('loading', true)
        vm.$http.get(api).then((res) => {
          if (res.data.success) {
            resolve([...res.data.products])
          } else {
            reject(res.data.message)
          }
          vm.emitter.emit('loading', false)
        }).catch((err) => {
          reject(err)
          vm.emitter.emit('loading', false)
        })
      })
    },
    getProductByCategory () {
      this.productFilter = this.productAll.filter((item) =>
        item.category === this.currentProduct.category && item.id !== this.currentProduct.id)
    },
    setThumbsSwiper (swiper) {
      this.thumbsSwiper = swiper
    },
    saveFavorite () {
      const favoriteList = JSON.parse(localStorage.getItem('favorite')) || []
      const id = this.currentProduct.id
      if (favoriteList.indexOf(id) === -1) {
        favoriteList.push(id)
        localStorage.setItem('favorite', JSON.stringify(favoriteList))
        this.emitter.emit('push-message', {
          style: 'success',
          title: '訊息通知',
          content: '收藏成功',
          icon: 'fas fa-check-circle'
        })
        this.checkFavorite()
      } else {
        this.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知268',
          content: '加入失敗，收藏已存在',
          icon: 'fas fa-exclamation-circle'
        })
      }
    },
    cancelFavorite () {
      const favoriteList = JSON.parse(localStorage.getItem('favorite')) || []
      const index = favoriteList.indexOf(this.currentProduct.id)
      if (index) {
        favoriteList.splice(index, 1)
        localStorage.setItem('favorite', JSON.stringify(favoriteList))
        this.checkFavorite()
        this.emitter.emit('push-message', {
          style: 'success',
          title: '訊息通知',
          content: '取消收藏成功',
          icon: 'fas fa-check-circle'
        })
      } else {
        this.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知290',
          content: '取消失敗，找不到收藏',
          icon: 'fas fa-exclamation-circle'
        })
      }
    },
    addToCart (item) {
      console.log('addToCart')
      const vm = this
      const token = document.cookie.replace(/(?:(?:^|.*;\s*)hexToken\s*=\s*([^;]*).*$)|^.*$/, '$1')
      vm.$http.defaults.headers.common.Authorization = `Bearer ${token}`
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/cart`
      const data = { product_id: item.id, qty: item.num }
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
            title: '錯誤通知315',
            content: res.data.message.join('、'),
            icon: 'fas fa-exclamation-circle'
          })
        }
        vm.emitter.emit('loading', false)
      }).catch((err) => {
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知324',
          content: err,
          icon: 'fas fa-exclamation-circle'
        })
        vm.emitter.emit('loading', false)
      })
    },
    fetchInfo () {
      console.log('fetchInfo')
      const vm = this
      vm.productId = this.$route.params.id
      vm.emitter.emit('cartVisible', true)
      Promise.all([vm.getProductDetail(), vm.getProductAll()]).then((res) => {
        vm.currentProduct = { ...res[0], num: 1 }
        vm.imgs = [...vm.currentProduct.imagesUrl]
        vm.productAll = res[1]
        vm.getProductByCategory()
        const menuItem = document.querySelectorAll('.dropdown-item')
        menuItem.forEach((item) => {
          item.classList.remove('active')
          if (item.textContent === vm.currentProduct.category) {
            item.classList.add('active')
          }
        })
        vm.checkFavorite()
      }).catch((reject) => {
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知351',
          content: reject,
          icon: 'fas fa-exclamation-circle'
        })
      })
    },
    checkFavorite () {
      const favoriteList = JSON.parse(localStorage.getItem('favorite')) || []
      const id = favoriteList.indexOf(this.currentProduct.id)
      if (id !== -1) {
        this.isFavorite = true
      } else {
        this.isFavorite = false
      }
    }
  },
  created () {
    this.fetchInfo()
  }
}
</script>
