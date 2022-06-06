<template>
  <div class="toast bg-light" role="alert" aria-live="assertive" aria-atomic="true" ref="toast">
    <div class="toast-header">
      <img :src="require('@/assets/images/logo.png')" class="rounded me-2" alt="logo">
      <strong class="me-auto">{{ msg.title }}</strong>
      <span><i :class="[`text-${msg.style}`, `${msg.icon}`]"></i></span>
      <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
    </div>
    <div class="toast-body">
      {{ msg.content }}
    </div>
  </div>
</template>

<script>
import Toast from 'bootstrap/js/dist/toast'

export default {
  name: 'Message',
  props: [
    'msg'
  ],
  mounted () {
    const toastEl = this.$refs.toast
    const toast = new Toast(toastEl, {
      delay: 3200
    })
    toast.show()
  }
}
</script>
