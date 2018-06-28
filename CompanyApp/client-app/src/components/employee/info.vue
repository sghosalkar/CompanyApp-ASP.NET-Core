<template>
  <div class="panel panel-primary">
    <div class="panel-heading">
      {{ employee.Id }} - {{ employee.Name }}
    </div>
    <div class="panel-body" v-if="showInfo">
      {{ employee.Department.Name }}
      <a @click="onHideInfo">Hide</a>
    </div>
    <div class="panel-footer" v-if="!showInfo">
      <div>
        <a @click="onShowInfo">Info</a>
        <a @click="onDelete">Delete</a>
      </div>
    </div>
  </div>
</template>

<script>
  import axios from 'axios';

  export default {
    props: ['employee'],
    data() {
      return {
        showInfo: false,
      }
    },
    methods: {
      onShowInfo() {
        this.showInfo = true;
      },
      onHideInfo() {
        this.showInfo = false;
      },
      getInfo() {
        axios.get('http://localhost:50011/Employees/Details', { params: { Id: this.employee.Id } })
          .then((response) => {
            console.log(response.data)
            this.employee = response.data;
          })
          .catch(
            (error) => console.log(error)
          );
      },
      onDelete() {
        axios.delete('http://localhost:50011/Employees/Delete', { params: { Id: this.employee.Id } })
          .then((response) => {
            console.log(response.data)
          })
          .catch(
            (error) => console.log(error)
          );
      }
    },
    created() {
      //this.getInfo();
    },
  }
</script>

<style scoped>
  a {
    cursor: pointer;
    text-decoration-color:blue;
    text-decoration: underline;
  }
</style>
