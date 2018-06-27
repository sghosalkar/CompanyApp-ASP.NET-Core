<template>
  <div>
    <h1>Departments</h1>
    <app-new-department></app-new-department>
    <app-list-department></app-list-department>
  </div>
</template>

<script>

  import NewDepartment from './new-department.vue';
  import ListDepartment from './list.vue';

  export default {
    data() {
      return {}
    },
    components: {
      'app-new-department': NewDepartment,
      'app-list-department': ListDepartment
    }
  }

</script>
