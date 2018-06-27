<template>
  <div class="panel panel-primary">
    <div class="panel-heading">
      {{ department.Id }} {{ department.Name }}
    </div>
    <div class="panel-footer">
      <div>
        <a href="" @click="onEdit">Edit</a>
        <a href="" @click="onDelete">Delete</a>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    props: ['department'],
    methods: {
      onEdit() {

      },
      onDelete() {

      }
    },
  }
</script>
