<template>
  <div>
    <app-info-project v-for="project in projects" :project="project" :key="project.Id">
    </app-info-project>
  </div>
</template>

<script>
  import axios from 'axios';
  import InfoProject from './info.vue';

  export default {
    data() {
      return {
        projects: null,
      };
    },
    components: {
      'app-info-project': InfoProject,
    },
    methods: {
      getProjects() {
        axios.get('http://localhost:50011/Projects/GetAll')
          .then((response) => {
            this.projects = response.data;
            console.log(response.data)
          })
          .catch(
            (error) => console.log(error)
          );
      }
    },
    created() {
      this.getProjects();
    },
  }
</script>
