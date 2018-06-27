<template>
  <div>
    <h1>Projects</h1>
    <app-new-project></app-new-project>
    <app-list-project></app-list-project>
  </div>
</template>

<script>

  import NewProject from './new-project.vue';
  import ListProject from './list.vue';

  export default {
    data() {
      return {}
    },
    components: {
      'app-new-project': NewProject,
      'app-list-project': ListProject
    }
  }

</script>
