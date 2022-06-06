<template>
  <div class="modal fade" ref="modal" id="articleModal" tabindex="-1" aria-labelledby="articleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-header bg-primary text-white">
          <h5 class="modal-title" id="articleModalLabel"><slot name="title"></slot></h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="row">
            <div class="col-12">
              <div class="mb-3">
                <label for="title" class="form-label">*標題</label>
                <input type="text" class="form-control" id="title" v-model="currentArticle.title"
                placeholder="請輸入標題">
              </div>
              <div class="mb-3">
                <label for="description" class="form-label">*描述</label>
                <textarea class="form-control" id="description" v-model="currentArticle.description" placeholder="請輸入描述"></textarea>
              </div>
              <div class="mb-3">
                <label for="tag" class="form-label">*分類標籤</label>
                <select name="tag" id="tag" multiple v-model="currentArticle.tag" class="form-select">
                  <option value="" disabled>請選擇</option>
                  <option value="客廳">客廳</option>
                  <option value="廚房">廚房</option>
                  <option value="飯廳">飯廳</option>
                  <option value="臥房">臥房</option>
                  <option value="浴室">浴室</option>
                  <option value="工作室">工作室</option>
                  <option value="陽台">陽台</option>
                </select>
              </div>
              <div class="mb-3">
                <label for="code" class="form-label">*圖片</label>
                <input type="file" class="form-control" id="code" ref="imageUpload" @change="updateImage">
                <img :src="currentArticle.image" :alt="currentArticle.title" class="img-fluid w-50">
              </div>
              <div class="mb-3">
                <label for="author" class="form-label">*作者</label>
                <input type="text" class="form-control" id="author" v-model="currentArticle.author"
                placeholder="請輸入作者">
              </div>
              <div class="mb-3">
                <label for="content" class="form-label">*內容</label>
                <textarea class="form-control" id="content" v-model="currentArticle.content"
                placeholder="請輸入內容"></textarea>
              </div>
              <div class="mb-3">
                <div class="form-check">
                  <input class="form-check-input" type="checkbox" id="isPublic" v-model="currentArticle.isPublic">
                  <label class="form-check-label" for="isPublic">
                  *是否公開
                  </label>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-outline-dark" data-bs-dismiss="modal">取消</button>
          <button type="button" class="btn btn-primary btn-hv-style" @click="updateArticle(currentArticle)">儲存</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import modalMixin from '@/mixin/ModalMixin.js'

export default {
  name: 'articleMainModal',
  props: {
    currentItem: {
      type: Object
    }
  },
  mixins: [modalMixin],
  data () {
    return {
      currentArticle: {}
    }
  },
  watch: {
    currentItem () {
      this.currentArticle = this.currentItem
    }
  },
  methods: {
    updateArticle (item) {
      this.$emit('updateArticle', this.currentArticle)
    },
    updateImage () {
      const vm = this
      const formData = new FormData()
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/upload`
      const uploadFile = this.$refs.imageUpload.files[0]
      formData.append('file-to-upload', uploadFile)
      vm.$http.post(api, formData).then((res) => {
        if (res.data.success) {
          vm.currentArticle.image = res.data.imageUrl
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知106',
            content: res.data.message,
            icon: 'fas fa-exclamation-circle'
          })
        }
      }).catch((err) => {
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知114',
          content: err,
          icon: 'fas fa-exclamation-circle'
        })
      })
    }
  }
}
</script>
