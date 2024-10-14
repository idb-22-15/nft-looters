<script setup lang="ts">
import { toTypedSchema } from '@vee-validate/zod'
import { useForm } from 'vee-validate'
import { z } from 'zod'
import { validationErrors } from '~/src/shared/config'

const schema = z.object({
  email: z.string({ message: validationErrors.required }).email(),
  password: z
    .string({ message: validationErrors.required })
    .min(6, { message: validationErrors.minChars(6) })
    .max(256, { message: validationErrors.maxChars(256) }),
  repeatPassword: z
    .string({ message: validationErrors.required })
    .min(6, { message: validationErrors.minChars(6) })
    .max(256, { message: validationErrors.maxChars(256) }),
}).refine(v => v.repeatPassword === v.password, { path: ['repeatPassword'], message: 'Пароли не совпадают' })

const { values, handleSubmit, meta } = useForm({ validationSchema: toTypedSchema(schema) })
const onSubmit = handleSubmit((values) => {
  console.log(values)
})
</script>

<template>
  <div class="mx-8 mt-6 mb-8">
    <h1 class="text-4xl font-semibold">
      Регистрация
    </h1>
    <form
      class="mt-4 flex max-w-3xl flex-col gap-y-4"
      @submit.prevent="onSubmit"
    >
      <FormField
        v-slot="{ componentField }"
        name="email"
      >
        <FormItem>
          <FormLabel>Email</FormLabel>
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
          <FormLabel>Пароль</FormLabel>
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
          <FormLabel>Повторите пароль</FormLabel>
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
        :disabled="!(meta.dirty && meta.valid)"
        type="submit"
      >
        Войти
      </Button>
      <span>Есть аккаунт? <NuxtLink
        class="font-semibold"
        to="/auth/login"
      >Войти</NuxtLink></span>
    </form>
  </div>
</template>
