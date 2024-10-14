<script setup lang="ts">
import { toTypedSchema } from '@vee-validate/zod'
import { useForm } from 'vee-validate'
import { z } from 'zod'
import { ethers, type Provider, type Signer } from 'ethers'

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
  login: z.string().min(1),
})

const { values, handleSubmit } = useForm({ validationSchema: toTypedSchema(schema) })
const onSubmit = handleSubmit((values) => {
  console.log(values)
  login()
})
</script>

<template>
  <div class="mx-8 mt-6 mb-8">
    {{ signer }}
    <h1>Вход</h1>
    <form @submit.prevent="onSubmit">
      <FormField
        v-slot="{ componentField }"
        name="login"
      >
        <FormItem>
          <FormLabel>Логин</FormLabel>
          <FormControl>
            <Input v-bind="componentField" />
          </FormControl>
          <FormMessage />
        </FormItem>
      </FormField>
      <Button type="submit">
        Войти
      </Button>
    </form>
  </div>
</template>
