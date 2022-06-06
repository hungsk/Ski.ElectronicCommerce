<template>
  <div id="carouselCaptions" class="carousel slide" data-bs-ride="carousel">
    <div class="carousel-inner">
      <div class="carousel-item active">
        <img v-lazy="require('@/assets/images/banner1.jpg')" class="d-block w-100 carousel-img" alt="banner1">
        <div class="carousel-caption d-flex justify-content-end align-items-end">
          <div class="d-none d-md-block carousel-caption-content">
            <h2 class="h1 fw-bold mb-3 text-start ps-2">DEMO空間</h2>
            <p class="text-start ps-2 h4 fw-bold">提供廠商透明充裕的資訊</p>
            <p class="text-start ps-2 h4 fw-bold">滿足客戶多樣化的需求</p>
          </div>
        </div>
      </div>
      <div class="carousel-item">
        <img v-lazy="require('@/assets/images/banner2.jpg')" class="d-block w-100 carousel-img carousel-img-left" alt="banner2">
          <div class="carousel-caption d-flex justify-content-end align-items-end">
          <div class="d-none d-md-block carousel-caption-content">
            <h2 class="h1 fw-bold mb-3 text-start ps-2">實用☓美學</h2>
            <p class="text-start ps-2 h4 fw-bold">貼近需求的建材OO</p>
            <p class="text-start ps-2 h4 fw-bold">打造理想舒適的OO設計</p>
          </div>
        </div>
      </div>
      <div class="carousel-item">
        <img v-lazy="require('@/assets/images/banner3.jpg')" class="d-block w-100 carousel-img carousel-img-right" alt="banner3">
        <div class="carousel-caption d-flex justify-content-end align-items-end">
          <div class="d-none d-md-block carousel-caption-content">
            <h2 class="h1 fw-bold mb-3 text-start ps-2">多元☓透明</h2>
            <p class="text-start ps-2 h4 fw-bold">多元的空間OO</p>
            <p class="text-start ps-2 h4 fw-bold">合理透明的資訊管道</p>
          </div>
        </div>
      </div>
    </div>
    <button class="carousel-control-prev" type="button" data-bs-target="#carouselCaptions" data-bs-slide="prev">
      <span class="carousel-control-prev-icon" aria-hidden="true"></span>
      <span class="visually-hidden">Previous</span>
    </button>
    <button class="carousel-control-next" type="button" data-bs-target="#carouselCaptions" data-bs-slide="next">
      <span class="carousel-control-next-icon" aria-hidden="true"></span>
      <span class="visually-hidden">Next</span>
    </button>
  </div>
</template>

<script>
export default {
  name: 'Carousel'
}
</script>
