<template>
  <div>
    <nav class="navbar navbar-expand-md navbar-dark bg-dark">
      <div class="container-fluid">
        <router-link class="ms-3 navbar-brand" to="/">
          <img src="@/assets/images/logo.png" class="me-2" alt="logo"/>
          <span class="text-white">DEMO空間</span>
        </router-link>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse"
        data-bs-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent" aria-expanded="false"
        aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav me-auto mb-2 mb-lg-0">
            <li class="nav-item">
              <router-link class="nav-link" to="/admin/productManage">產品管理</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" to="/admin/orderManage">訂單管理</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" to="/admin/couponManage">優惠卷管理</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" to="/admin/articleManage">文章管理</router-link>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#" @click.prevent="signOut">登出</a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  </div>
</template>

<script>
export default {
  name: 'NavbarManage',
  methods: {
    signOut () {
      const vm = this
      const api = `${process.env.VUE_APP_API}/logout`
      vm.emitter.emit('loading', true)
      vm.$http.post(api).then((res) => {
        if (res.data.success) {
          const { token, expired } = res.data
          document.cookie = `hexToken=${token};expires=${new Date(expired)};`
          vm.$router.push('/login')
        }
      }).catch((err) => {
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知54',
          content: err,
          icon: 'fas fa-exclamation-circle'
        })
        vm.emitter.emit('loading', false)
      })
    }
  }
}
</script>
