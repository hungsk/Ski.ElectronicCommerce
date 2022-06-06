<template>
  <div class="container">
    <PageTitle :path="require('@/assets/images/page-title11.png')">結帳-填寫資料</PageTitle>
    <div class="row mh-wrapper">
      <div class="col-12 py-3">
        <div class="row justify-content-center">
          <div class="col-md-8">
            <CheckoutProgress :step = "2"></CheckoutProgress>
          </div>
        </div>
        <div class="bg-light p-5">
          <Form @submit="ConfirmSend" v-slot="{ errors }" class="row g-4">
            <div class="col-md-6 mb-3">
              <label for="userName" class="form-label">
                <span class="text-danger">*</span>姓名:
              </label>
              <Field type="text" name="姓名" id="userName" class="form-control"
              v-model="user.name" :class="{ 'is-invalid': errors['姓名'] }" rules="required"
              placeholder="請輸入姓名"></Field>
              <ErrorMessage name="姓名" class="invalid-feedback"></ErrorMessage>
            </div>
            <div class="col-md-6 mb-3">
              <label for="email" class="form-label">
                <span class="text-danger">*</span>email:
              </label>
              <Field type="email" name="email" id="email" class="form-control"
              v-model="user.email" :class="{ 'is-invalid': errors['email'] }"
              rules="email|required" placeholder="example@gmail.com"></Field>
              <ErrorMessage name="email" class="invalid-feedback"></ErrorMessage>
            </div>
            <div class="col-md-6 mb-3">
              <label for="tel" class="form-label">
                <span class="text-danger">*</span>電話:
              </label>
              <Field type="text" name="電話" id="tel" class="form-control" v-model="user.tel"
              :class="{ 'is-invalid': errors['電話'] }" rules="digits:10|required"
              placeholder="0920123456"></Field>
              <ErrorMessage name="電話" class="invalid-feedback"></ErrorMessage>
            </div>
            <div class="col-md-6 mb-3">
              <label for="addr" class="form-label">
                <span class="text-danger">*</span>地址:
              </label>
              <Field type="text" name="地址" id="addr" class="form-control" v-model="user.address"
              :class="{ 'is-invalid': errors['地址'] }" rules="required"
              placeholder="新北市新店區安康路二段100號3樓"></Field>
              <ErrorMessage name="地址" class="invalid-feedback"></ErrorMessage>
            </div>
            <div class="col-12 mb-3">
              <label for="msg" class="form-label">留言/備註:</label>
              <textarea name="msg" id="msg" class="form-control" v-model="message"></textarea>
            </div>
            <div class="col-12 text-end">
              <button type="submit" class="btn btn-lg btn-major">
                下一步，結帳付款<i class="fas fa-shipping-fast ms-2"></i>
              </button>
            </div>
          </Form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import PageTitle from '@/components/Front/PageTitle.vue'
import CheckoutProgress from '@/components/Front/CheckoutProgress.vue'

export default {
  name: 'CheckoutInfo',
  components: {
    PageTitle,
    CheckoutProgress
  },
  data () {
    return {
      user: {
        email: '',
        name: '',
        tel: '',
        address: ''
      },
      message: ''
    }
  },
  methods: {
    createOrder () {
      console.log('createOrder')
      const vm = this
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/order`
      const info = { user: vm.user, message: vm.message, payment_method: vm.payment_method }
      console.log(info)
      vm.emitter.emit('loading', true)
      vm.$http.post(api, { data: info }).then((res) => {
        if (res.data.success) {
          vm.emitter.emit('getCart')
          vm.emitter.emit('RefreshCart')
          const orderId = res.data.orderId
          vm.$router.push(`/checkoutPay/${orderId}`)
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
    },
    ConfirmSend () {
      const vm = this
      vm.$swal({
        title: '確認送出?',
        text: '送出後即無法更改聯絡資訊',
        showCancelButton: true,
        confirmButtonText: '確認送出！',
        cancelButtonText: '取消',
        icon: 'info'
      }).then((result) => {
        if (result.isConfirmed) {
          vm.createOrder()
        }
      })
    }
  },
  created () {
    this.emitter.emit('cartVisible', false)
  }
}
</script>
