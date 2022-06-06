<template>
  <loading :active="isLoading" :is-full-page="fullPage">
    <div class="d-flex">
      <div class="cube cube-blue cube1"></div>
      <div class="cube cube-deepblue cube2"></div>
      <div class="cube cube-blue cube3"></div>
    </div>
    <div class="d-flex">
      <div class="cube cube-red cube2"></div>
      <div class="cube cube-orange cube3"></div>
      <div class="cube cube-green cube4"></div>
    </div>
    <div class="d-flex">
      <div class="cube cube-yellow cube3"></div>
      <div class="cube cube-green cube4"></div>
      <div class="cube cube-red cube5"></div>
    </div>
  </loading>
</template>

<script>
export default {
  name: 'LoadingItem',
  data () {
    return {
      fullPage: true,
      isLoading: this.$root.LoadingStatus
    }
  },
  created () {
    this.emitter.on('loading', (status) => {
      this.$root.LoadingStatus = status
      this.isLoading = this.$root.LoadingStatus
    })
  }
}
</script>
