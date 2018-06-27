<template>
  <div>
    <h1>Roles</h1>
    <app-new-role></app-new-role>
    <app-list-role></app-list-role>
  </div>
</template>

<script>

  import NewRole from './new-role.vue';
  import ListRole from './list.vue';

  export default {
    data() {
      return {}
    },
    components: {
      'app-new-role': NewRole,
      'app-list-role': ListRole
    }
  }

</script>
