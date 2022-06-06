<template>
  <div class="container">
    <div class="row">
      <div class="col-12 py-5 text-center">
        <h1>訂單管理</h1>
      </div>
      <div class="col-12">
        <div class="table-responsive">
          <table class="table" v-if="orders.length>0">
            <thead>
              <tr>
                <th>編號</th>
                <th>建立時間</th>
                <th>總金額</th>
                <th>客戶姓名</th>
                <th>是否付款</th>
                <th>編輯</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in orders" :key="item.id">
                <td>{{ item.id }}</td>
                <td>{{ $filters.date(item.create_at) }}</td>
                <td>{{ item.total }}</td>
                <td>{{ item.user.name }}</td>
                <td>
                  <span v-if="item.is_paid" class="text-success">已付款</span>
                  <span v-if="!item.is_paid">未付款</span>
                </td>
                <td>
                  <button type="button" class="btn btn-outline-primary me-2" @click="openModal(item)">
                    修改
                  </button>
                  <button type="button" class="btn btn-outline-dark" @click="openDeleteModal(item)">
                    刪除
                  </button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      <div class="col-12">
        <BackPagination :pageItem="pageItem" @updatePage="getOrders"/>
      </div>
    </div>
    <MainModal ref="modalMain" :currentItem="currentOrder" @updateOrder="updateOrder">
      <template v-slot:title>{{ modalTitle }}</template>
    </MainModal>
    <DeleteModal ref="modalDelete" :currentItem="currentOrder" @deleteItem="deleteOrder">
      <template v-slot:title>{{ modalTitle }}</template>
    </DeleteModal>
  </div>
</template>

<script>
import DeleteModal from '@/components/Back/DeleteModal.vue'
import MainModal from '@/components/Back/OrderMainModal.vue'
import BackPagination from '@/components/Back/BackPagination.vue'

export default {
  name: 'OrderManage',
  components: {
    DeleteModal,
    MainModal,
    BackPagination
  },
  data () {
    return {
      orders: [],
      currentOrder: {},
      pageItem: {},
      modalTitle: ''
    }
  },
  methods: {
    getOrders (page = 1) {
      console.log('getOrders')
      const vm = this
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/orders?page=${page}`
      vm.emitter.emit('loading', true)
      vm.$http.get(api).then((res) => {
        if (res.data.success) {
          vm.orders = JSON.parse(JSON.stringify(res.data.orders))
          vm.pageItem = { ...res.data.pagination }
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
    openModal (item) {
      console.log('openModal')
      this.currentOrder = JSON.parse(JSON.stringify(item))
      console.log(this.currentOrder)
      this.modalTitle = '修改訂單'
      this.$refs.modalMain.showModal()
    },
    openDeleteModal (item) {
      this.currentOrder = { ...item, title: `訂單: ${item.id}` }
      this.modalTitle = '刪除訂單'
      this.$refs.modalDelete.showModal()
    },
    deleteOrder (id) {
      const vm = this
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/order/${id}`
      vm.emitter.emit('loading', true)
      vm.$refs.modalDelete.hideModal()
      vm.$http.delete(api).then((res) => {
        if (res.data.success) {
          vm.emitter.emit('push-message', {
            style: 'success',
            title: '訊息通知',
            content: '訂單更新成功',
            icon: 'fas fa-check-circle'
          })
          vm.getOrders(vm.pageItem.currentPage)
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
        vm.emitter.emit('loading', false)
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知',
          content: err,
          icon: 'fas fa-exclamation-circle'
        })
      })
    },
    updateOrder (item) {
      console.log('updateOrder')
      console.log(item)
      const vm = this
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/order/${item.id}`
      vm.emitter.emit('loading', true)
      vm.$refs.modalMain.hideModal()
      vm.$http.put(api, { data: item }).then((res) => {
        if (res.data.success) {
          vm.emitter.emit('push-message', {
            style: 'success',
            title: '訊息通知',
            content: '訂單更新成功',
            icon: 'fas fa-check-circle'
          })
          vm.getOrders(vm.pageItem.currentPage)
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知',
            content: res.data.message.join('、'),
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
    this.getOrders()
  }
}
</script>
