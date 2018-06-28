<template>
  <form @submit.prevent="onSubmitted">
    <div class="form-group">
      <label for="name">Name</label>
      <input type="text" id="name" class="form-control" v-model="employee.Name" />
    </div>
    <div class="form-group">
      <label for="department">Department</label>
      <select id="department" class="form-control custom-select" v-model="employee.Department">
        <option v-for="department in allDepartments" v-bind:value="department"> {{ department.Name }}</option>
      </select>
    </div>
    <div class="form-group">
      <label for="role">Role</label>
      <select id="role" class="form-control custom-select" v-model="employee.Role">
        <option v-for="role in allRoles" v-bind:value="role"> {{ role.Name }}</option>
      </select>
    </div><div class="form-group">
      <label for="password">Password</label>
      <input type="password" id="password" class="form-control" pattern="[A-Za-z\d]{4,}" v-model="employee.Password" />
    </div>
    <div class="form-group">
      <label for="confirm-password">Confirm Password</label>
      <input type="password" id="confirm-password" class="form-control" pattern="[A-Za-z\d]{4,}" v-model="employee.ConfirmPassword" />
    </div>
    <div class="form-group">
      <input type="submit" class="btn btn-primary" value="Create" />
    </div>

  </form>
</template>

<script>
  import axios from 'axios';

  export default {
    data() {
      return {
        employee: {
          Name: null,
          Department: null,
          Role: null,
          Password: null,
          ConfirmPassword: null
        },
        allDepartments: null,
        allRoles: null,
      }
    },
    methods: {
      async onSubmitted() {
        //this.employee.Department = JSON.parse(JSON.stringify(this.employee.Department));
        //this.employee.Role = JSON.parse(JSON.stringify(this.employee.Role));
        const add = this.$store.state.data.add;
        console.log(this.employee);
        const getById = this.$store.state.data.getById;
        var Department = await getById(this.$store.state.data.Departments, { params: { Id: this.employee.Department.Id } });
        console.log(Department);
        //add(this.$store.state.data.Employees, this.employee);
      },
      async loadSelectors() {
        const getAll = this.$store.state.data.getAll;
        this.allDepartments = await getAll(this.$store.state.data.Departments);
        this.allRoles = await getAll(this.$store.state.data.Roles);
      }
    },
    mounted() {
      this.loadSelectors();
    },
  }
</script>
