import { useProfileStore } from '~/src/shared/model/user'

export default defineNuxtRouteMiddleware((to) => {
  const profileStore = useProfileStore()
  if (to.path.startsWith('/profile/user') && !profileStore.isUserProfile) return navigateTo('/auth/login')
  if (to.path.startsWith('/profile/organization') && !profileStore.isOrganizationProfile) return navigateTo('/auth/login')
})
