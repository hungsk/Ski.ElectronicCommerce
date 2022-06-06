<template>
  <div class="toast-container position-fixed top-0 end-0 px-3 pt-4">
    <Toast v-for="(msg, key) in messages" :key="key" :msg="msg"/>
  </div>
</template>

<script>
import Toast from '@/components/Share/Message.vue'

export default {
  name: 'MessageList',
  components: { Toast },
  data () {
    return {
      messages: []
    }
  },
  mounted () {
    this.emitter.on('push-message', (message) => {
      const { style, title, content, icon } = message
      this.messages.push({ style, title, content, icon })
    })
  }
}
</script>

<style scope>
.toast-container{
  z-index: 999
}
</style>
