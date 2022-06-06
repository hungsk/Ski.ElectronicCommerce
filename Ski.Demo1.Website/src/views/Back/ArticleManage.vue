<template>
  <div class="container">
    <div class="row">
      <div class="col-12 py-5 text-center">
        <h1>文章管理</h1>
      </div>
      <div class="col-12 text-end py-3">
        <button type="button" class="btn btn-primary btn-hv-style" @click="openModal(true)">
          新增文章
        </button>
      </div>
      <div class="col-12">
        <div class="table-responsive">
          <table class="table" id="table-product" v-if="articleAll.length>0">
            <thead>
              <tr>
                <th>名稱</th>
                <th>創立時間</th>
                <th>公開/私人</th>
                <th>編輯</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in articleAll" :key="item.id">
                <td>{{ item.title }}</td>
                <td>{{ $filters.date(item.create_at) }}</td>
                <td>{{ item.isPublic ? '公開' : '私人' }}</td>
                <td>
                  <button type="button" class="btn btn-outline-primary me-2" @click="openModal(false, item)">修改</button>
                  <button type="button" class="btn btn-outline-dark" @click="openDeleteModal(item)">刪除</button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      <div class="col-12">
        <BackPagination :pageItem="pageItem" @updatePage="getArticle"/>
      </div>
    </div>
    <MainModal ref="modalMain" :currentItem="currentArticle" @updateArticle="updateArticle">
      <template v-slot:title>{{ modalTitle }}</template>
    </MainModal>
    <DeleteModal ref="modalDelete" :currentItem="currentArticle" @deleteItem="deleteArticle">
      <template v-slot:title>{{ modalTitle }}</template>
    </DeleteModal>
  </div>
</template>

<script>
import MainModal from '@/components/Back/ArticleMainModal.vue'
import DeleteModal from '@/components/Back/DeleteModal.vue'
import BackPagination from '@/components/Back/BackPagination.vue'

export default {
  name: 'ArticleManage',
  components: {
    MainModal,
    DeleteModal,
    BackPagination
  },
  data () {
    return {
      articleAll: [],
      modalTitle: '',
      isNew: true,
      currentArticle: {},
      pageItem: {}
    }
  },
  methods: {
    getArticle (page = 1) {
      const vm = this
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/articles?page=${page}`
      vm.emitter.emit('loading', true)
      vm.$http.get(api).then((res) => {
        if (res.data.success) {
          vm.articleAll = [...res.data.articles]
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
    openModal (isNew, item) {
      const vm = this
      if (isNew) {
        vm.modalTitle = '新增文章'
        vm.currentArticle = { isPublic: false }
        vm.isNew = isNew
        vm.$refs.modalMain.showModal()
      } else {
        const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/article/${item.id}`
        vm.modalTitle = '修改文章'
        vm.$http.get(api).then((res) => {
          if (res.data.success) {
            vm.currentArticle = { ...res.data.article }
            vm.isNew = isNew
            vm.$refs.modalMain.showModal()
          } else {
            vm.emitter.emit('push-message', {
              style: 'danger',
              title: '錯誤通知',
              content: res.data.message,
              icon: 'fas fa-exclamation-circle'
            })
          }
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
    openDeleteModal (item) {
      this.currentArticle = { ...item }
      this.modalTitle = '刪除文章'
      this.$refs.modalDelete.showModal()
    },
    updateArticle (item) {
      const vm = this
      let api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/article`
      let httpMethod = 'post'
      const article = { ...item, create_at: Math.floor(new Date() / 1000) }
      if (!this.isNew) {
        api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/article/${item.id}`
        httpMethod = 'put'
      }
      vm.emitter.emit('loading', true)
      vm.$refs.modalMain.hideModal()
      vm.$http[httpMethod](api, { data: article }).then((res) => {
        if (res.data.success) {
          vm.emitter.emit('push-message', {
            style: 'success',
            title: '訊息通知',
            content: '文章更新成功',
            icon: 'fas fa-check-circle'
          })
          vm.getArticle(vm.pageItem.currentPage)
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
    },
    deleteArticle (id) {
      const vm = this
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/article/${id}`
      vm.$http.delete(api).then((res) => {
        if (res.data.success) {
          vm.$refs.modalDelete.hideModal()
          vm.emitter.emit('push-message', {
            style: 'success',
            title: '訊息通知',
            content: '文章刪除成功',
            icon: 'fas fa-check-circle'
          })
          vm.getArticle(vm.pageItem.currentPage)
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知',
            content: res.data.message,
            icon: 'fas fa-exclamation-circle'
          })
        }
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
    this.getArticle()
  }
}
</script>
