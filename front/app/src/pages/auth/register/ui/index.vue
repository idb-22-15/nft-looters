<script setup lang="ts">
import { toTypedSchema } from '@vee-validate/zod'
import { useForm } from 'vee-validate'

import { useOrganizationStore, useUserStore } from '~/src/shared/model/user'
import { Button } from '~/src/shared/ui/kit/button'
import { FormControl, FormField, FormItem, FormLabel, FormMessage } from '~/src/shared/ui/kit/form'
import { Input } from '~/src/shared/ui/kit/input'
import { Tabs, TabsContent, TabsList, TabsTrigger } from '~/src/shared/ui/kit/tabs'
import { useToast } from '~/src/shared/ui/kit/toast'

import { organizationSchema, userSchema } from '../config'

const userStore = useUserStore()
const organizationStore = useOrganizationStore()

const { toast } = useToast()

const { handleSubmit: handleUserSubmit, meta: userMeta }
= useForm({ validationSchema: toTypedSchema(userSchema) })

const onUserSubmit = handleUserSubmit(async (values) => {
  try {
    console.log('reg user', values)
    userStore.register(values)
    await navigateTo('/profile/user')
  }
  catch (_e) {
    toast({ title: 'Упс', variant: 'destructive' })
  }
})

const { handleSubmit: handleOrganizationSubmit, meta: organizationMeta }
= useForm({ validationSchema: toTypedSchema(organizationSchema) })

const onOrganizationSubmit = handleOrganizationSubmit(async (values) => {
  try {
    console.log('reg org', values)
    organizationStore.register(values)
    await navigateTo('/profile/organization')
  }
  catch (_e) {
    toast({ title: 'Упс', variant: 'destructive' })
  }
})
</script>

<template>
  <div class="mx-8 mt-6 mb-8">
    <h1 class="text-4xl font-semibold">
      Регистрация
    </h1>
    <Tabs
      class="mt-4"
      default-value="user"
    >
      <TabsList>
        <TabsTrigger value="user">
          Как личность
        </TabsTrigger>
        <TabsTrigger value="organization">
          Как организация
        </TabsTrigger>
      </TabsList>
      <TabsContent value="user">
        <form
          class="mt-4 flex max-w-3xl flex-col gap-y-4"
          @submit.prevent="onUserSubmit"
        >
          <FormField
            v-slot="{ componentField }"
            name="firstName"
          >
            <FormItem>
              <FormLabel>Имя *</FormLabel>
              <FormControl>
                <Input v-bind="componentField" />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>

          <FormField
            v-slot="{ componentField }"
            name="lastName"
          >
            <FormItem>
              <FormLabel>Фамилия *</FormLabel>
              <FormControl>
                <Input v-bind="componentField" />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>

          <FormField
            v-slot="{ componentField }"
            name="email"
          >
            <FormItem>
              <FormLabel>Email *</FormLabel>
              <FormControl>
                <Input v-bind="componentField" />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>

          <FormField
            v-slot="{ componentField }"
            name="password"
          >
            <FormItem>
              <FormLabel>Пароль *</FormLabel>
              <FormControl>
                <Input
                  v-bind="componentField"
                  type="password"
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>

          <FormField
            v-slot="{ componentField }"
            name="repeatPassword"
          >
            <FormItem>
              <FormLabel>Повторите пароль *</FormLabel>
              <FormControl>
                <Input
                  v-bind="componentField"
                  type="password"
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>
          <Button
            :disabled="!(userMeta.dirty && userMeta.valid)"
            type="submit"
          >
            Создать
          </Button>
          <span>Есть аккаунт? <NuxtLink
            class="font-semibold"
            to="/auth/login"
          >Войти</NuxtLink></span>
        </form>
      </TabsContent>
      <TabsContent value="organization">
        <form
          class="mt-4 flex max-w-3xl flex-col gap-y-4"
          @submit.prevent="onOrganizationSubmit"
        >
          <FormField
            v-slot="{ componentField }"
            name="name"
          >
            <FormItem>
              <FormLabel>Название организации *</FormLabel>
              <FormControl>
                <Input v-bind="componentField" />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>

          <FormField
            v-slot="{ componentField }"
            name="email"
          >
            <FormItem>
              <FormLabel>Email *</FormLabel>
              <FormControl>
                <Input v-bind="componentField" />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>

          <FormField
            v-slot="{ componentField }"
            name="password"
          >
            <FormItem>
              <FormLabel>Пароль *</FormLabel>
              <FormControl>
                <Input
                  v-bind="componentField"
                  type="password"
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>

          <FormField
            v-slot="{ componentField }"
            name="repeatPassword"
          >
            <FormItem>
              <FormLabel>Повторите пароль *</FormLabel>
              <FormControl>
                <Input
                  v-bind="componentField"
                  type="password"
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          </FormField>
          <Button
            :disabled="!(organizationMeta.dirty && organizationMeta.valid)"
            type="submit"
          >
            Создать
          </Button>
          <span>Есть аккаунт? <NuxtLink
            class="font-semibold"
            to="/auth/login"
          >Войти</NuxtLink></span>
        </form>
      </TabsContent>
    </Tabs>
  </div>
</template>
