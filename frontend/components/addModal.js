function addModal() {
    return {
        addForm: { },

        open() {
            Alpine.store('globalState').addModalOpen = true;
        },

        close() {
            Alpine.store('globalState').addModalOpen = false;
            this.addForm = { };
        },

        getActiveTab(){
          return Alpine.store('globalState').activeTab;  
        },

        async submit() {
            try {
                const store = Alpine.store('globalState');
                const token = store.currentUser.token;

                const url = this.getActiveTab() === 'hotels'
                    ? `http://localhost:8000/api/hotels`
                    : `http://localhost:8000/api/rooms`;

                const res = await fetch(url, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`
                    },
                    body: JSON.stringify(this.addForm)
                });

                if (!res.ok) throw new Error(await res.text());
                const data = await res.json();
                
                if (data.IsFailure){
                    alert(data.Error);
                } else {
                    alert('Updated successfully!');
                }

                if (this.getActiveTab() === 'hotels') await store.fetchHotels();
                else if(this.getActiveTab() === 'rooms') await store.fetchRooms();
                

                this.close();
            } catch (err) {
                alert('Error: ' + err.message);
            }
        },
    }
}