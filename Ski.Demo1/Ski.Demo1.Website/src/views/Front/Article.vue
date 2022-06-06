<template>
  <div class="container">
    <PageTitle :path="require('@/assets/images/page-title9.png')">私房設計推薦</PageTitle>
    <div class="row pt-4">
      <div class="col-md-12 d-flex justify-content-center align-items-center flex-wrap">
        <button type="button" class="btn btn-outline-dark rounded-pill me-2 mb-1"
        :class="{ active : selectItem === '全部' }" @click="getSelectArticle('全部')">全部
        </button>
        <button type="button" class="btn btn-outline-dark rounded-pill me-2 mb-1"
        :class="{ active : selectItem === '客廳' }" @click="getSelectArticle('客廳')">客廳
        </button>
        <button type="button" class="btn btn-outline-dark rounded-pill me-2 mb-1"
        :class="{ active : selectItem === '臥房' }" @click="getSelectArticle('臥房')">臥房
        </button>
        <button type="button" class="btn btn-outline-dark rounded-pill me-2 mb-1"
        :class="{ active : selectItem === '廚房' }" @click="getSelectArticle('廚房')">廚房
        </button>
        <button type="button" class="btn btn-outline-dark rounded-pill me-2 mb-1"
        :class="{ active : selectItem === '飯廳' }" @click="getSelectArticle('飯廳')">飯廳
        </button>
        <button type="button" class="btn btn-outline-dark rounded-pill me-2 mb-1"
        :class="{ active : selectItem === '工作室' }" @click="getSelectArticle('工作室')">工作室
        </button>
        <button type="button" class="btn btn-outline-dark rounded-pill me-2 mb-1"
        :class="{ active : selectItem === '陽台' }" @click="getSelectArticle('陽台')">陽台
        </button>
        <button type="button" class="btn btn-outline-dark rounded-pill"
        :class="{ active : selectItem === '浴室' }" @click="getSelectArticle('浴室')">浴室
        </button>
      </div>
    </div>
    <div class="mh-wrapper">
      <div class="row py-4" v-masonry transition-duration="0.3s" item-selector=".grid-item">
        <div class="col-sm-6 col-md-6 col-lg-4 col-xxl-3 mb-4 grid-item" v-masonry-tile
        v-for="item in articlefilter" :key="item.id">
          <router-link :to="`/article/content/${item.id}?tag=${selectItem}`">
            <div class="card shadow">
              <div class="card-img-wrapper">
                <img :src="item.image" class="card-img-top img-fluid" :alt="item.title">
                <div class="card-img-layer d-flex justify-content-center align-items-center">
                  <p class="h5">點選查看</p>
                </div>
              </div>
              <div class="card-body">
                <p class="card-title h5">{{ item.title }}</p>
                <p class="card-text"><small>{{ item.description }}</small></p>
              </div>
            </div>
          </router-link>
        </div>
      </div>
      <div class="py-1 text-end">
        <router-link to="/">
          <p class="h3">
            <i class="bi bi-arrow-left me-3"></i>回首頁
          </p>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
import PageTitle from '@/components/Front/PageTitle.vue'

export default {
  name: 'Article',
  components: {
    PageTitle
  },
  data () {
    return {
      articleAll: [],
      selectItem: '全部',
      articlefilter: []
    }
  },
  methods: {
    getArticleAll () {
      const vm = this
      const api1 = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/articles?page=1`
      const api2 = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/articles?page=2`
      const request1 = vm.$http.get(api1)
      const request2 = vm.$http.get(api2)
      vm.emitter.emit('loading', true)
      vm.$http.all([request1, request2]).then(vm.$http.spread((...res) => {
        if (res[0].data.success && res[1].data.success) {
          vm.articleAll = [...res[0].data.articles, ...res[1].data.articles]
          const { tag } = this.$route.query
          if (tag) {
            vm.selectItem = tag
          }
          vm.getSelectArticle(vm.selectItem)
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知',
            content: `${res[0].data.message},${res[1].data.message}`,
            icon: 'fas fa-exclamation-circle'
          })
        }
        vm.emitter.emit('loading', false)
      })).catch(err => {
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知',
          content: err,
          icon: 'fas fa-exclamation-circle'
        })
        vm.emitter.emit('loading', false)
      })
    },
    getSelectArticle (select) {
      this.selectItem = select
      switch (this.selectItem) {
        case '全部':
          this.articlefilter = [...this.articleAll]
          break
        case '客廳':
          this.articlefilter = this.articleAll.filter((item) => item.tag.includes(this.selectItem))
          break
        case '飯廳':
          this.articlefilter = this.articleAll.filter((item) => item.tag.includes(this.selectItem))
          break
        case '廚房':
          this.articlefilter = this.articleAll.filter((item) => item.tag.includes(this.selectItem))
          break
        case '臥房':
          this.articlefilter = this.articleAll.filter((item) => item.tag.includes(this.selectItem))
          break
        case '浴室':
          this.articlefilter = this.articleAll.filter((item) => item.tag.includes(this.selectItem))
          break
        case '陽台':
          this.articlefilter = this.articleAll.filter((item) => item.tag.includes(this.selectItem))
          break
        case '工作室':
          this.articlefilter = this.articleAll.filter((item) => item.tag.includes(this.selectItem))
          break
      }
    }
  },
  created () {
    this.emitter.emit('cartVisible', true)
    this.getArticleAll()
  }
}
</script>
