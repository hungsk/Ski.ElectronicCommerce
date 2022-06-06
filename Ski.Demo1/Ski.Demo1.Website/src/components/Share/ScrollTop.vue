<template>
  <div class="scroll-top" v-if="showScroll">
    <a href="#" @click.prevent="toTop">
      <img :src="require('@/assets/images/top.png')" alt="向上捲動">
    </a>
  </div>
</template>

<script>
export default {
  name: 'ScrollTop',
  data () {
    return {
      showScroll: false
    }
  },
  methods: {
    toTop () {
      window.scrollTo(0, 0)
    },
    scroll () {
      const scrollTop = window.pageYOffset || document.body.scrollTop || document.documentElement.scrollTop
      if (scrollTop > 20) {
        this.showScroll = true
      } else {
        this.showScroll = false
      }
    }
  },
  mounted () {
    window.addEventListener('scroll', this.scroll)
  },
  unmounted () {
    window.removeEventListener('scroll', this.scroll)
  }
}
</script>
