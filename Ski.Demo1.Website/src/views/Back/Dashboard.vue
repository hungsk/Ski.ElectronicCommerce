<template>
  <div>
    <MessageList/>
    <LoadingItem/>
    <ProductManage/>
    <router-view></router-view>
  </div>
</template>

<script>
import ProductManage from '@/components/Back/NavbarManage.vue'
import MessageList from '@/components/Share/MessageList.vue'
import LoadingItem from '@/components/Share/LoadingItem.vue'

export default {
  name: 'Dashboard',
  components: {
    ProductManage,
    MessageList,
    LoadingItem
  },
  created () {
    const vm = this
    const token = document.cookie.replace(/(?:(?:^|.*;\s*)hexToken\s*=\s*([^;]*).*$)|^.*$/, '$1')
    vm.$http.defaults.headers.common.Authorization = `Bearer ${token}`
    const api = `${process.env.VUE_APP_API}/api/user/check`
    console.log(api)
    vm.$http.post(api).then((res) => {
      console.log(res.data)
      if (!res.data.success) {
        vm.$router.push('/login')
      }
    }).catch((err) => {
      vm.emitter.emit('push-message', {
        style: 'danger',
        title: '錯誤通知34',
        content: err,
        icon: 'fas fa-exclamation-circle'
      })
      vm.$router.push('/login')
    })
  }
}
</script>
