<template>
  <div class="container">
    <PageTitle v-model:path="pageImage">地材 - {{ category }}</PageTitle>
    <div class="row mh-wrapper">
      <div class="col-12">
        <Menu :active="'地材'"/>
      </div>
      <div class="col-12 py-3">
        <div class="row">
          <div class="col-sm-6 col-md-4 col-lg-3 mb-4" v-for="item in productResult" :key="item.id">
            <ProductCard :data="item" :category="category" :categoryMain="'floor'" :page="pageItem.currentPage"/>
          </div>
        </div>
        <div class="row" v-if="pageItem.pageTotal!==1">
          <div class="col-12 py-3">
            <Pagination :pageItem="pageItem" :data="productFilter" @changePage="goToPage"/>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import PageTitle from '@/components/Front/PageTitle.vue'
import Menu from '@/components/Front/Menu.vue'
import ProductCard from '@/components/Front/ProductCard.vue'
import Pagination from '@/components/Share/Pagination.vue'
import ProductMixin from '@/mixin/ProductMixin.js'
import $ from 'jquery'

export default {
  name: 'Floor',
  components: {
    PageTitle,
    Menu,
    ProductCard,
    Pagination
  },
  mixins: [ProductMixin],
  data () {
    return {
      category: '',
      productAll: [],
      productFilter: [],
      productResult: [],
      pageItem: {},
      pageImage: require('@/assets/images/loading.png'),
      page: 1
    }
  },
  watch: {
    $route (to, from) {
      const kind = to.params.floor
      this.page = to.query.page
      this.category = kind
      this.getProductByCategory(this.page)
    }
  },
  methods: {
    getProductByCategory (page = 1) {
      switch (this.category) {
        case '木地板':
          this.productFilter = this.productAll.filter((item) => item.category === '木地板')
          $('.page-img').fadeOut(1)
          $('.page-title-layer').fadeOut(1)
          this.pageImage = require('@/assets/images/page-title4.png')
          $('.page-img').fadeIn(2000)
          $('.page-title-layer').fadeIn(2000)
          break
        case '石塑地板':
          this.productFilter = this.productAll.filter((item) => item.category === '石塑地板')
          $('.page-img').fadeOut(1)
          $('.page-title-layer').fadeOut(1)
          this.pageImage = require('@/assets/images/page-title14.png')
          $('.page-img').fadeIn(2000)
          $('.page-title-layer').fadeIn(2000)
          break
        case '塑膠地板':
          this.productFilter = this.productAll.filter((item) => item.category === '塑膠地板')
          $('.page-img').fadeOut(1)
          $('.page-title-layer').fadeOut(1)
          this.pageImage = require('@/assets/images/page-title15.png')
          $('.page-img').fadeIn(2000)
          $('.page-title-layer').fadeIn(2000)
          break
      }
      page = Number(page)
      const result = this.$pagination(this.productFilter, this.productResult, page)
      this.pageItem = result[0]
      this.productResult = result[1]
    },
    goToPage (page) {
      const result = this.$pagination(this.productFilter, this.productResult, page)
      this.pageItem = result
    }
  },
  created () {
    const category = this.$route.params.floor
    this.category = category
    this.page = this.$route.query.page
    this.emitter.emit('cartVisible', true)
    this.getProductAll()
  }
}
</script>
