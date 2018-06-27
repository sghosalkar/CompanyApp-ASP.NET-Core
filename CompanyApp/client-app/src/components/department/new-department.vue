<template>
  <form @submit.prevent="onSubmitted">
    <div class="form-group">
      <label for="name" class="control-label">Name</label>
      <input type="text" id="department-name" class="form-control" v-model="Name" />
    </div>
    <div class="form-group">
      <input type="submit" value="Create" class="btn btn-default" />
    </div>
  </form>
</template>


<script>
  import axios from 'axios';

  export default {
    data() {
      return {
        Name: ''
      }
    },
    methods: {
      onSubmitted() {
        axios.post('http://localhost:50011/Departments/Create',
          { Name: this.Name })
          .then(
          (response) => console.log(response)
          )
          .catch(
          (error) => console.log(error)
        );

      }
    }
  }
</script>
