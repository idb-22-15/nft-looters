<script setup lang="ts">
import { toTypedSchema } from '@vee-validate/zod'
import { ethers, type Provider, type Signer } from 'ethers'
import { useForm } from 'vee-validate'

import { useOrganizationStore, useUserStore } from '~/src/shared/model/user'
import { Button } from '~/src/shared/ui/kit/button'
import { FormControl, FormField, FormItem, FormLabel, FormMessage } from '~/src/shared/ui/kit/form'
import { Input } from '~/src/shared/ui/kit/input'
import { Tabs, TabsList, TabsTrigger } from '~/src/shared/ui/kit/tabs'
import { useToast } from '~/src/shared/ui/kit/toast'

import { loginSchema } from '../config'

const userStore = useUserStore()
const organizationStore = useOrganizationStore()

const { toast } = useToast()

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

const profileType = ref<'user' | 'organization'>('user')

const { handleSubmit, meta } = useForm({ validationSchema: toTypedSchema(loginSchema) })
const onSubmit = handleSubmit(async (values) => {
  console.log(values)
  try {
    if (profileType.value === 'user') {
      userStore.login(values)
      await navigateTo('/profile/user')
    }
    else {
      organizationStore.login(values)
      await navigateTo('/profile/organization')
    }
  }
  catch (_e) {
    toast({ title: 'Упс', variant: 'destructive' })
  }
})
</script>

<template>
  <div class="mx-8 mt-6 mb-8">
    {{ signer }}
    <h1 class="text-4xl font-semibold">
      Вход
    </h1>
    <Tabs
      v-model:model-value="profileType"
      class="mt-4"
    >
      <TabsList>
        <TabsTrigger value="user">
          Как личность
        </TabsTrigger>
        <TabsTrigger value="organization">
          Как организация
        </TabsTrigger>
      </TabsList>
    </Tabs>
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
