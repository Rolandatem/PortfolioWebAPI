using PortfolioWebAPI.Data.Models;
using PortfolioWebAPI.Interfaces;

namespace PortfolioWebAPI.Data.Seeders;

internal class FAQsDataSeeder(
    PortfolioDbContext _context) : IDataSeeder
{
    public async Task SeedAsync()
    {
        await _context.FAQs.AddRangeAsync([
            new FAQ() {
                Question = "When did Martinez Flooring start?",
                Answer = "Martinez Flooring launched in 2000 with humble beginning.",
                IsSiteReady = true
            },
            new FAQ() {
                Question = "What forms of payment are accepted?",
                Answer = "We accept all major credit cards (Visa, MasterCard, Discover, American Express), PayPal, Amazon Pay, Flooring Inc. gift cards, and checks by mail.",
                IsSiteReady = true
            },
            new FAQ() {
                Question = "Do you offer financing options?",
                Answer = "Absolutely! With Affirm, you can pay over 3, 6, or 12 months with 0-36% APR. Check your eligibility.",
                IsSiteReady = true
            },
            new FAQ() {
                Question = "Do you price match?",
                Answer = "Yes, we do! If you find the same item at a lower price from a qualifying retailer, we'll match it. Find out how.",
                IsSiteReady = true
            },
            new FAQ() {
                Question = "Do you offer military discounts?",
                Answer = "Discounts are available for active-duty military and front-line workers. Give us a call at (800) 555-1212 to discuss your best offers.",
                IsSiteReady = true
            },
            new FAQ() {
                Question = "What is the Estimated Ship Date?",
                Answer = "This is when your product is expected to ship from our warehouse or one of our partners.",
                IsSiteReady = true
            },
            new FAQ() {
                Question = "What if I need items fast?",
                Answer = "Need it quick? Items labeled Next Day Ship will go out the next business day if you order by 2pm EST. Quick Ship items are out in 1-3 days, with most deliveries arriving within 3 days.",
                IsSiteReady = true
            },
            new FAQ() {
                Question = "What is your return policy?",
                Answer = "We accept returns on items in original, resellable condition. Request a refund within 30 days by calling (800) 555-5555 or reaching out via chat or email.",
                IsSiteReady = true
            },
            new FAQ() {
                Question = "How do I check my order status?",
                Answer = "You can check your order status by logging into your account and going to \"My Orders\" or entering in your order details into the form.",
                IsSiteReady = true
            },
            new FAQ() {
                Question = "How do I modify or cancel my order?",
                Answer = "Call us right away. If your order has already started production or shipping, changes may involve fees, so be sure to act fast.",
                IsSiteReady = true
            },
            new FAQ() {
                Question = "How do I contact Customer Service?",
                Answer = "We're here to help! Call us at (800) 555-5555, email customerservice@martinezflooring.com.",
                IsSiteReady = true
            },
            new FAQ() {
                Question = "How do I order free samples?",
                Answer = "To order samples, visit any product page, select up to 3, and mix and match from different categories. Click \"Order Free Samples\" to add them to your cart.",
                IsSiteReady = true
            },
            new FAQ() {
                Question = "How can I get coupon codes or information on upcoming promotions?",
                Answer = "Join our newsletter to stay in the loop on our latest deals, new arrivals, and helpful tips! See footer below.",
                IsSiteReady = true
            }
        ]);

        await _context.SaveChangesAsync();
    }        
}