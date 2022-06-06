<template>
  <div class="container">
    <PageTitle :path="require('@/assets/images/page-title1.png')">管理員登入</PageTitle>
    <div class="row justify-content-center align-items-center mh-wrapper">
      <div class="col-md-6 my-3 rounded">
        <form @submit.prevent="login">
          <div class="form-floating mb-3">
            <input type="email" class="form-control" v-model="user.username"
            id="username" name="username" placeholder="name@example.com">
            <label for="username">帳號</label>
          </div>
          <div class="form-floating">
            <input type="password" class="form-control" v-model="user.password"
            id="password" name="password" placeholder="密碼">
            <label for="password">密碼</label>
          </div>
          <div class="pt-3 d-grid">
            <button type="submit" class="btn btn-major">登入</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import PageTitle from '@/components/Front/PageTitle.vue'

export default {
  name: 'Login',
  components: {
    PageTitle
  },
  data () {
    return {
      user: {
        username: '',
        password: ''
      }
    }
  },
  methods: {
    login () {
      const vm = this
      const api = `${process.env.VUE_APP_API}/admin/signin`
      vm.emitter.emit('loading', true)
      vm.$http.post(api, vm.user).then((res) => {
        if (res.data.success) {
          const { token, expired } = res.data
          document.cookie = `hexToken=${token};expires=${new Date(expired)};`
          vm.emitter.emit('push-message', {
            style: 'success',
            title: '訊息通知',
            content: '登入成功',
            icon: 'fas fa-check-circle'
          })
          vm.$router.push('/admin/productManage')
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
    this.emitter.emit('cartVisible', true)
  }
}
</script>
