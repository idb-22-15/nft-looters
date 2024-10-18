import { useProfileStore } from '~/src/shared/model/user'

export default defineNuxtRouteMiddleware(async (to, from) => {
  const profileStore = useProfileStore()
  // await profileStore.load()

  // const { promise, resolve } = Promise.withResolvers()
  // console.log('route', profileStore.status)

  // profileStore.$subscribe(() => {
  //   console.log(profileStore.status)
  //   if (profileStore.status === 'success' || profileStore.status === 'error') {
  //     resolve(undefined)
  //   }
  // }, { immediate: true })

  // watch(() => profileStore.status, () => {
  //   console.log(profileStore.status)
  //   if (profileStore.status === 'success' || profileStore.status === 'error') {
  //     resolve(undefined)
  //   }
  // }, { immediate: true })
  // await promise

  if (to.path.startsWith('/profile/user') && !profileStore.isUserProfile) return navigateTo('/auth/login')
  if (to.path.startsWith('/profile/organization') && !profileStore.isOrganizationProfile) return navigateTo('/auth/login')
})
