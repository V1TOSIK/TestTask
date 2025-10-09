document.addEventListener("alpine:init", () => {
  Alpine.data("header", () => ({
    tabs: ['hotels', 'rooms', 'bookings'],

    setActiveTab(tab){
      Alpine.store('globalState').activeTab = tab;
    },

    getActiveTab(){
      return Alpine.store('globalState').activeTab;
    },

    open() {
      Alpine.store('globalState').loginModalOpen = true;
    },

    logout() {
      localStorage.setItem('token', ''),
      Alpine.store('globalState').currentUser = {
        id: '',
        role: '',
        token: '',
      },
      alert('✅ Ви успішно вийшли!');
    },

  }));
});