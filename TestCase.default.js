describe('//##TEST_CASE_NAME##', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
    })

    afterEach(() => {
        cy.signOut()
    })

    it('//##TEST_CASE_NAME##', () => {
        //##TEST_STEPS##
    })
})

