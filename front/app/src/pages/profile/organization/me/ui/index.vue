<!-- eslint-disable vue/no-ref-object-reactivity-loss -->
<script setup lang="ts">
import { toTypedSchema } from '@vee-validate/zod'
import { VisuallyHidden } from 'radix-vue'
import { useForm } from 'vee-validate'
import { z } from 'zod'

import { useAuthenticatedOrganization, useOrganizationStore } from '~/src/shared/model/user'
import { Button } from '~/src/shared/ui/kit/button'
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle, DialogTrigger } from '~/src/shared/ui/kit/dialog'
import { FormControl, FormField, FormItem, FormLabel, FormMessage } from '~/src/shared/ui/kit/form'
import { Input } from '~/src/shared/ui/kit/input'
import MetaMaskIcon from '~/src/shared/ui/MetaMaskIcon.vue'

import { mockPhotos } from '../__mocks__'

defineOptions({
  name: 'ProfilePage',
})

const organizationStore = useOrganizationStore()
const organization = useAuthenticatedOrganization()

const errorRequired = 'Это обязательное поле'

const schema = z.object({
  avatar: z.string().url().optional(),
  name: z.string({ message: errorRequired }).min(1, { message: errorRequired }).max(256),
})

const { values, handleSubmit, meta } = useForm({ validationSchema: toTypedSchema(schema), initialValues: {
  avatar: organization.value.avatar,
  name: organization.value.name,
} })

const onSubmit = handleSubmit((values) => {
  console.log('update org')
})
</script>

<template>
  <div class="mx-8 mt-6 mb-8">
    <h1 class="text-4xl font-semibold hidden">
      Личный кабинет
    </h1>
    <div class="grid items-center mt-4 grid-cols-[max-content_1fr] gap-x-4">
      <div class="">
        <img
          v-if="values.avatar"
          alt="avatar"
          class="object-cover w-32 aspect-square rounded-full"
          :src="values.avatar"
        >
        <div
          v-else
          class="w-32 flex items-center justify-center aspect-square rounded-full bg-slate-100"
        >
          {{ organization.name.slice(0, 2) }}
        </div>
      </div>
      <div class="">
        <Dialog>
          <DialogTrigger :as-child="true">
            <Button variant="outline">
              Выбрать из NFT
            </Button>
          </DialogTrigger>

          <DialogContent
            class="max-h-[100svh] min-w-[90vw] rounded-lg"
          >
            <VisuallyHidden>
              <DialogHeader>
                <DialogTitle>NFT</DialogTitle>
                <DialogDescription>NFT</DialogDescription>
              </DialogHeader>
            </VisuallyHidden>
            <div class="grid grid-cols-5 gap-4">
              <img
                v-for="photo in mockPhotos"
                :key="photo"
                alt=""
                class="cursor-pointer rounded-lg aspect-square object-cover"
                :src="photo"
              >
            </div>
          </DialogContent>
        </Dialog>
        <!-- <Input type="file" /> -->
      </div>
    </div>
    <form
      class="flex mt-6 flex-col max-w-[500px] gap-y-4"
      @submit.prevent="onSubmit"
    >
      <FormField
        v-slot="{ componentField }"
        name="name"
      >
        <FormItem>
          <FormLabel>Название</FormLabel>
          <FormControl>
            <Input v-bind="componentField" />
          </FormControl>
        </FormItem>
        <FormMessage />
      </FormField>

      <Button
        :disabled="!(meta.dirty && meta.valid)"
        type="submit"
      >
        Сохранить
      </Button>

      <Button
        class="mt-0 w-full flex items-center"
        type="button"
        variant="secondary"
      >
        Подключить <MetaMaskIcon class="h-10" />
      </Button>
    </form>

    <Button
      class="mt-8"
      type="button"
      variant="destructiveSecondary"
      @click="organizationStore.logout()"
    >
      Выйти из аккаунта
    </Button>
  </div>
</template>
