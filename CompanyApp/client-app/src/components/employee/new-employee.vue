<template>
  <form @submit.prevent="onSubmitted">
    <div class="form-group">
      <label for="name">Name</label>
      <input type="text" id="employee-name" class="form-control" v-model="employeeName" />
    </div>
    <button class="btn btn-primary" type="submit">Submit</button>
  </form>
</template>

<script>
  import axios from 'axios';

  export default {
    data() {
      return {
        employeeName: ''
      }
    },
    methods: {
      onSubmitted() {
        console.log(this.employeeName);
        axios.post('http://localhost:50011/Employees/VueInsert',
          { Name: this.employeeName })
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
