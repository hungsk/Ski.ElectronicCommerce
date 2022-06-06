<template>
  <div class="container">
    <PageTitle :path="require('@/assets/images/page-title6.png')">新品到貨</PageTitle>
    <div class="row mh-wrapper">
      <div class="col-12 py-5">
        <div class="pb-5 text-center">
          <p class="h5">看膩了一成不變的傳統建材嗎?</p>
          <p class="h5">DEMO空間精選新款建材，打造潮流居家!</p>
        </div>
        <div class="row">
          <div class="col-sm-6 col-md-4 col-lg-3 mb-4" v-for="item in NewJson" :key="item.id">
            <ProductCard :data="item"/>
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
  </div>
</template>

<script>
import PageTitle from '@/components/Front/PageTitle.vue'
import ProductCard from '@/components/Front/ProductCard.vue'
import NewJson from '@/assets/json/New.json'

export default {
  name: 'New',
  components: {
    PageTitle,
    ProductCard
  },
  data () {
    return {
      NewJson
    }
  },
  created () {
    this.emitter.emit('cartVisible', true)
  }
}
</script>
