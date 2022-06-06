<template>
  <ul class="d-flex progress list-unstyled">
    <li class="progress-item" :class="{'active':step === 1}">確認訂單</li>
    <li class="progress-item" :class="{'active':step === 2}">填寫資料</li>
    <li class="progress-item" :class="{'active':step === 3}">結帳付款</li>
  </ul>
</template>

<script>
export default {
  name: 'CheckoutProgress',
  props: {
    step: {
      type: Number
    }
  }
}
</script>
