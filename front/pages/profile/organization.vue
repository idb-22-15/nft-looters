<script setup lang="ts">
import { useAuthenticatedUser } from '~/src/shared/model/user'

const user = useAuthenticatedUser()
</script>

<template>
  <div class="flex flex-col mx-8 mt-6 mb-8">
    <h1 class="text-4xl font-semibold">
      Организация
    </h1>
    <div
      v-if="user.organization"
      class=""
    >
      <h2 class="mt-3 text-2xl font-semibold">
        {{ user.organization.name }}
      </h2>
    </div>
    <div
      v-else
      class=""
    >
      В данный момент вы не состоите в организации
    </div>
  </div>
</template>
