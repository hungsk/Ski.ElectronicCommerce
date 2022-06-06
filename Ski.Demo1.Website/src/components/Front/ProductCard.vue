<template>
  <router-link :to="`/product/${data.id}?categoryMain=${categoryMain}&category=${category}&page=${page}`">
    <div class="card h-100 shadow">
      <div class="card-img-wrapper">
        <img v-lazy="data.imageUrl" class="card-img-top img-card" :alt="data.title">
        <div class="card-img-layer d-flex justify-content-center align-items-center">
          <p class="h5">點選查看</p>
        </div>
      </div>
      <div class="card-body">
        <p class="card-title h5 mb-1">{{ data.title }}</p>
        <div class="d-flex justify-content-between align-items-center pb-1">
          <span class="card-text card-gray-text"><small>{{ data.description }}</small></span>
          <span class="badge rounded-pill bg-success" v-if="data.price != data.origin_price">特價</span>
        </div>
        <div class="d-flex justify-content-between align-items-center pb-1">
          <span class="card-text text-danger"><strong>{{ $filters.currency(data.price) }}</strong></span>
          <span class="card-text" v-if="data.price != data.origin_price"><del>{{ $filters.currency(data.origin_price) }}</del></span>
        </div>
      </div>
    </div>
  </router-link>
</template>

<script>
export default {
  name: 'ProductCard',
  props: {
    data: {
      type: Object
    },
    category: {
      type: String,
      default: ''
    },
    categoryMain: {
      type: String,
      default: ''
    },
    page: {
      type: [Number, String],
      default: 1
    }
  }
}
</script>
