import { mockUser } from '../__mocks__'

export type Gender = 'male' | 'female'

export type User = {
  id: string
  avatar?: string
  firstName: string
  lastName: string
  gender: Gender
  birthday: string
  organization?: UserOrganization
}

export type UserOrganization = {
  id: string
  name: string
  employees: Array<Omit<User, 'organization'>>
}

export const useUserStore = defineStore('user', () => {
  const user = ref<User | null>(null)

  const load = async () => {
    console.log('load user')
    user.value = mockUser
  }

  const update = async (newUser: User) => {
    user.value = newUser
  }

  const login = async (_password: string) => {
    user.value = mockUser
  }

  const logout = async () => {
    user.value = null
  }

  return {
    user, load, update, login, logout,
  }
})

export const useAuthenticatedUser = () => {
  const { user } = storeToRefs(useUserStore())

  return computed(() => {
    if (!user.value) throw new Error('Пользователь должен быть аутентифицирован')
    return user.value
  })
}
