<template>
  <div class="modal fade" ref="modal" id="deleteModalModal" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header bg-danger text-white">
          <h5 class="modal-title" id="deleteModalLabel"><slot name="title"></slot></h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          確定刪除 <strong>{{ currentItem.title }}</strong>?
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-outline-dark btn-hv-style" data-bs-dismiss="modal">取消</button>
          <button type="button" class="btn btn-danger btn-hv-style" @click="deleteItem(currentItem.id)">確定</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import modalMixin from '@/mixin/ModalMixin.js'

export default {
  name: 'DeleteModal',
  mixins: [modalMixin],
  props: {
    currentItem: {
      type: Object
    }
  },
  methods: {
    deleteItem (id) {
      this.$emit('deleteItem', id)
    }
  }
}
</script>
