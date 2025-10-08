document.addEventListener('alpine:init', () =>{
    Alpine.data('asideSection', () => ({
        open: false,
        localFilteredCities: [],
        localFilteredDate: null,
        cities: ['Kyiv', 'Lviv', 'Odesa', 'Rivne'],

        getActiveTab(){
          return Alpine.store('globalState').activeTab;  
        },
        
        applyFilters() {
            const store = Alpine.store('globalState');
            store.filteredCities = this.localFilteredCities;
            store.filteredDate = this.localFilteredDate;

            if (this.getActiveTab() === 'hotels') store.fetchHotels();
            else if (this.getActiveTab() === 'rooms') store.fetchRooms();

            this.open = false;
        }
    }));
});