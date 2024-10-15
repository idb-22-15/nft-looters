<script setup lang="ts">
import { toTypedSchema } from '@vee-validate/zod'
import { ethers, type Provider, type Signer } from 'ethers'
import { useForm } from 'vee-validate'
import { z } from 'zod'

import { Button } from '~/src/shared/ui/kit/button'
import { FormField, FormControl, FormLabel, FormItem, FormMessage } from '~/src/shared/ui/kit/form'
import { Input } from '~/src/shared/ui/kit/input'

const useSigner = () => {
  const provider = ref<Provider | null>(null)
  const signer = ref<Signer | null>(null)

  const login = async () => {
    if (window.ethereum == null) {
      console.log('MetaMask not installed; using read-only defaults')
      provider.value = ethers.getDefaultProvider()
    }
    else {
      await window.ethereum.request({ method: 'eth_requestAccounts' })
      provider.value = new ethers.BrowserProvider(window.ethereum)
      signer.value = await provider.value.getSigner()

      const userAddress = await signer.value.getAddress()
      console.log('Адрес пользователя:', userAddress)

      // Получаем текущий баланс пользователя
      const balance = await provider.value.getBalance(userAddress)
      console.log('Баланс пользователя:', ethers.formatEther(balance), 'ETH')
    }
  }
  return {
    signer, login,
  }
}

const { signer, login } = useSigner()

const schema = z.object({
  email: z.string().email(),
  password: z.string().min(6).max(256),
})

const { handleSubmit, meta, defineField } = useForm({ validationSchema: toTypedSchema(schema) })
const onSubmit = handleSubmit((values) => {
  console.log(values)
  login()
})
defineField('password', { validateOnModelUpdate: false })
</script>

<template>
  <div class="mx-8 mt-6 mb-8">
    {{ signer }}
    <h1 class="text-4xl font-semibold">
      Вход
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
              type="password"
              v-bind="componentField"
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
      <span>Нет аккаунта? <NuxtLink
        class="font-semibold"
        to="/auth/register"
      >Зарегистрироваться</NuxtLink></span>
    </form>
  </div>
</template>
