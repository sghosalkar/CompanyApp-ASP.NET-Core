<template>
  <div>
    <h1>Employees</h1>
    <app-new-employee></app-new-employee>
    <app-list-employee></app-list-employee>
  </div>
</template>

<script>

  import NewEmployee from './new-employee.vue';
  import ListEmployee from './list.vue';

  export default {
    data() {
      return {}
    },
    components: {
      'app-new-employee': NewEmployee,
      'app-list-employee': ListEmployee
    }
  }

</script>
