function loginModal() {
  return {
  activeTab: 'register',
  registerForm: { Credential: '', Password: '' },
  loginForm: { Credential: '', Password: '' },

  open() {
    Alpine.store('globalState').loginModalOpen = true;
    this.activeTab = 'register';
  },

  close() {
    Alpine.store('globalState').loginModalOpen = false;
    this.registerForm = { Credential: '', Password: '' };
    this.loginForm = { Credential: '', Password: '' };
  },

  async submit() {
    try {
      const url = this.activeTab === 'register' 
      ? `http://localhost:8000/api/auth/register`
      : `http://localhost:8000/api/auth/login`;

      const resBody = this.activeTab === 'register'
      ? JSON.stringify(this.registerForm)
      : JSON.stringify(this.loginForm)

      const res = await fetch(url, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: resBody
      });

      if (!res.ok) throw new Error(await res.text());
      const data = await res.json();

      localStorage.setItem('token', data.value.token);

      if (data.value.isFailure) alert(data.value.error);

      const userData = this.parseJwt(data.value.token);

      const idClaim = userData?.id
      || userData?.Id
      || userData?.sub
      || userData?.["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

      const roleClaim = userData?.role
      || userData?.Role
      || userData?.["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

      if (!idClaim) throw new Error("Cannot get UserId from token");

      Alpine.store('globalState').currentUser = {
      id: idClaim,
      role: roleClaim || 'User',
      token: data.value.token
      }

      if (this.activeTab == 'register')
        alert('✅ Successful Register!')
      else
        alert('✅ Successful Enter!');

      this.close();
    } catch (err) {
      alert('Enter error: ' + err.message);
    }
  },



  parseJwt(token) {
    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = decodeURIComponent(
        atob(base64)
        .split('')
        .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
      );
      return JSON.parse(jsonPayload);
    } catch (err) {
      console.error('Помилка парсингу токена:', err);
      return null;
    }
  }};
}