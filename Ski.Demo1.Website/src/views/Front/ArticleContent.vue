<template>
  <div class="container">
    <div class="row mh-wrapper">
      <div class="col-md-12 py-3">
        <a href="#" @click.prevent="historyBack">
          <i class="bi bi-arrow-left h1"></i>
        </a>
      </div>
      <div class="col-md-12">
        <h1 class="text-center mb-5 h2">
          <strong>{{ currentArticle.title }}</strong>
        </h1>
        <div class="d-flex justify-content-center">
          <img :src="currentArticle.image" :alt="currentArticle.title" class="img-fluid w-100">
        </div>
        <div class="my-3 py-3">
          <p class="h5 article-text">{{ currentArticle.content }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ArticleContent',
  data () {
    return {
      articleId: '',
      currentArticle: {}
    }
  },
  methods: {
    historyBack () {
      const { tag } = this.$route.query
      if (tag) {
        this.$router.push(`/article?tag=${tag}`)
      } else {
        this.$router.go(-1)
      }
    },
    getArticleContent () {
      const vm = this
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/article/${this.articleId}`
      vm.emitter.emit('loading', true)
      vm.$http.get(api).then((res) => {
        if (res.data.success) {
          vm.currentArticle = { ...res.data.article }
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知',
            content: res.data.message,
            icon: 'fas fa-exclamation-circle'
          })
        }
        vm.emitter.emit('loading', false)
      }).catch((err) => {
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知',
          content: err,
          icon: 'fas fa-exclamation-circle'
        })
        vm.emitter.emit('loading', false)
      })
    }
  },
  created () {
    this.articleId = this.$route.params.id
    this.emitter.emit('cartVisible', true)
    this.getArticleContent()
  }
}
</script>
