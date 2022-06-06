export function addQuantity (num) {
  if (num < 20) {
    num++
  }
  return num
}
export function minusQuantity (num) {
  if (num > 1) {
    num--
  }
  return num
}
export function verifyNumber (num) {
  if (num > 20) {
    num = 20
  }
  if (num < 1) {
    num = 1
  }
  return num
}

export function changeProductQty (item) {
  console.log('changeProductQty')
  const vm = this
  const token = document.cookie.replace(/(?:(?:^|.*;\s*)hexToken\s*=\s*([^;]*).*$)|^.*$/, '$1')
  vm.$http.defaults.headers.common.Authorization = `Bearer ${token}`
  const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/cart/${item.id}`
  const cart = {
    product_id: item.product_id,
    qty: item.qty
  }
  console.log(cart)
  vm.loadingItem = item.id
  vm.$http.put(api, { data: cart }).then((res) => {
    if (res.data.success) {
      vm.emitter.emit('getCart')
      vm.emitter.emit('RefreshCart')
    } else {
      vm.emitter.emit('push-message', {
        style: 'danger',
        title: '錯誤通知38',
        content: res.data.message,
        icon: 'fas fa-exclamation-circle'
      })
    }
    vm.loadingItem = ''
  }).catch((err) => {
    vm.emitter.emit('push-message', {
      style: 'danger',
      title: '錯誤通知47',
      content: err,
      icon: 'fas fa-exclamation-circle'
    })
    vm.emitter.emit('loading', false)
  })
}

export function deleteProduct (item) {
  const vm = this
  const token = document.cookie.replace(/(?:(?:^|.*;\s*)hexToken\s*=\s*([^;]*).*$)|^.*$/, '$1')
  vm.$http.defaults.headers.common.Authorization = `Bearer ${token}`
  const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/cart/${item.id}`
  vm.emitter.emit('loading', true)
  vm.$http.delete(api).then((res) => {
    if (res.data.success) {
      vm.emitter.emit('push-message', {
        style: 'success',
        title: '訊息通知',
        content: '商品已從購物車移除',
        icon: 'fas fa-check-circle'
      })
      vm.emitter.emit('getCart')
      vm.emitter.emit('RefreshCart')
    } else {
      vm.emitter.emit('push-message', {
        style: 'danger',
        title: '錯誤通知72',
        content: res.data.message,
        icon: 'fas fa-exclamation-circle'
      })
    }
    vm.emitter.emit('loading', false)
  }).catch((err) => {
    vm.emitter.emit('push-message', {
      style: 'danger',
      title: '錯誤通知81',
      content: err,
      icon: 'fas fa-exclamation-circle'
    })
    vm.emitter.emit('loading', false)
  })
}
export function getCart () {
  console.log('getCart')
  const vm = this
  const token = document.cookie.replace(/(?:(?:^|.*;\s*)hexToken\s*=\s*([^;]*).*$)|^.*$/, '$1')
  vm.$http.defaults.headers.common.Authorization = `Bearer ${token}`
  const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/cart`
  console.log(api)
  vm.emitter.emit('loading', true)
  vm.$http.get(api).then((res) => {
    console.log(res.data)
    if (res.data.success) {
      console.log(res.data)
      vm.cart = JSON.parse(JSON.stringify(res.data.data.carts))
      vm.total = res.data.data.total
      vm.final_total = res.data.data.final_total
    } else {
      vm.emitter.emit('push-message', {
        style: 'danger',
        title: '錯誤通知100',
        content: res.data.message,
        icon: 'fas fa-exclamation-circle'
      })
    }
    vm.emitter.emit('loading', false)
  }).catch((err) => {
    vm.emitter.emit('push-message', {
      style: 'danger',
      title: '錯誤通知109',
      content: err,
      icon: 'fas fa-exclamation-circle'
    })
    vm.emitter.emit('loading', false)
  })
}
