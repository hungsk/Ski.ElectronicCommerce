import $ from 'jquery'
export default {
  methods: {
    scroll () {
      const scrollPos = $(window).scrollTop()
      const windowH = $(window).height()
      $('.animated-x').each((index, element) => {
        const thisPos = $(element).offset().top
        if ((windowH + scrollPos) >= thisPos) {
          $(element).addClass('slidein')
        }
      })
    }
  },
  created () {
    window.addEventListener('scroll', this.scroll)
  },
  unmounted () {
    window.removeEventListener('scroll', this.scroll)
  }
}
