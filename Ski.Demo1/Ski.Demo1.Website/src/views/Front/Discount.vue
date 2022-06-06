<template>
  <div class="container">
    <PageTitle :path="require('@/assets/images/page-title8.png')">限時優惠</PageTitle>
    <div class="row mh-wrapper">
      <div class="col-12 py-5">
        <div class="pb-5 text-center">
          <p class="h5">想要節省支出又兼顧時尚美觀?</p>
          <p class="h5">精選商品限時優惠，經濟實惠的商品任您挑選</p>
        </div>
        <div class="row">
          <div class="col-sm-6 col-md-4 col-lg-3 mb-4" v-for="item in DiscountJson" :key="item.id">
            <ProductCard :data="item"></ProductCard>
          </div>
        </div>
        <div class="py-1 text-end">
          <router-link to="/">
            <p class="h3"><i class="bi bi-arrow-left me-3"></i>回首頁</p>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import PageTitle from '@/components/Front/PageTitle.vue'
import ProductCard from '@/components/Front/ProductCard.vue'
import DiscountJson from '@/assets/json/Discount.json'

export default {
  name: 'Discount',
  components: {
    PageTitle,
    ProductCard
  },
  data () {
    return {
      DiscountJson
    }
  },
  created () {
    this.emitter.emit('cartVisible', true)
  }
}
</script>
