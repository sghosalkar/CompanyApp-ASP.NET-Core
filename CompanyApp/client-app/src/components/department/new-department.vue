<template>
  <form @submit.prevent="onSubmitted">
    <div class="form-group">
      <label for="name" class="control-label">Name</label>
      <input type="text" id="department-name" class="form-control" v-model="department.Name" />
    </div>
    <div class="form-group">
      <input type="submit" value="Create" class="btn btn-default" />
    </div>
  </form>
</template>


<script>
  export default {
    data() {
      return {
        department: {
          Name: null
        }
      }
    },
    methods: {
      onSubmitted() {
        var entity = this.$store.state.data.Departments;
        const add = this.$store.state.data.add;
        add(entity, this.department);
      }
    }
  }
</script>
