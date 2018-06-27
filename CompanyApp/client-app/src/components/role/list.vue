<template>
  <div>
    <ul>
      <li v-for="role in roles">
        {{ role.name }}
      </li>
    </ul>
  </div>
</template>
<script>
  import axios from 'axios';

  export default {
    data() {
      return {
        roles: null,
      };
    },
    components: {
    },
    methods: {
      getRoles() {
        axios.get('http://localhost:50011/Roles/')
          .then((response) => {
            this.roles = response.data;
            console.log(response.data)
          })
          .catch(
            (error) => console.log(error)
          );
      }
    },
    created() {
      this.getRoles();
    },
  }
</script>
